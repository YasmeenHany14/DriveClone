using System.Security.Claims;
using AutoMapper;
using DriveClone.Application.Common.Constants.Errors;
using DriveClone.Application.Common.ErrorAndResults;
using DriveClone.Application.Contracts;
using DriveClone.Application.DTOs.AuthDtos;
using DriveClone.Domain.Models;
using DriveClone.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace DriveClone.Application.Services;

public class AuthService(
    IGenerateTokenService generateTokenService,
    IUnitOfWork unitOfWork,
    UserManager<User> userManager,
    IMapper mapper
) : IAuthService{
        public async Task<Result<User>> RegisterAsync(CreateUserAppDto registerDto)
    {
        var user = mapper.Map<User>(registerDto);
        var identityResult = await userManager.CreateAsync(user, registerDto.Password);
        if (user.Id == null || !identityResult.Succeeded)
            return Result<User>.Failure(CommonErrors.InternalServerError());
        return Result<User>.Success(user);
    }

    public async Task<Result<AuthTokensResponse>> LoginAsync(LoginDto userDto)
    {
        var user = await userManager.FindByEmailAsync(userDto.Email);
        
        var isValid = user != null && await userManager.CheckPasswordAsync(user, userDto.Password);
        if (!isValid)
            return Result<AuthTokensResponse>.Failure(AuthErrors.WrongCredentials());
        if (!user.IsActive)
            return Result<AuthTokensResponse>.Failure(AuthErrors.UserNotActive);
        
        var response = await GenerateTokenResponse(user);
        if (!response.IsSuccess)
            return Result<AuthTokensResponse>.Failure(response.Error);
        
        await unitOfWork.RefreshTokenRepository.AddAsync(new RefreshToken
        {
            Token = response.Value.RefreshToken,
            UserId = user.Id,
            ExpiryDate = response.Value.RefreshExpiresAt
        });
        var result = await unitOfWork.SaveChangesAsync();
        
        return result <= 0 ? Result<AuthTokensResponse>.Failure(CommonErrors.InternalServerError()) : response;
    }

    public async Task<Result<AuthTokensResponse>> RefreshTokenAsync(string accessToken, string refreshToken)
    {
        var principal = await generateTokenService.GetPrincipalFromExpiredToken(accessToken);
        var userId = principal.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (string.IsNullOrEmpty(userId))
        {
            await unitOfWork.RefreshTokenRepository.RevokeToken(refreshToken);
            return Result<AuthTokensResponse>.Failure(CommonErrors.InvalidRefreshToken());
        }
        
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return Result<AuthTokensResponse>.Failure(CommonErrors.InvalidRefreshToken());
            
        var token = await unitOfWork.RefreshTokenRepository.CheckTokenExistsByUserId(refreshToken, userId);
        if (token == null || token.ExpiryDate < DateTime.UtcNow || token.IsRevoked)
            return Result<AuthTokensResponse>.Failure(CommonErrors.InvalidRefreshToken());
        
        var response = await GenerateTokenResponse(user);
        if (!response.IsSuccess)
            return Result<AuthTokensResponse>.Failure(response.Error);
        
        unitOfWork.RefreshTokenRepository.ReplaceToken(token, response.Value.RefreshToken, response.Value.RefreshExpiresAt);
        await unitOfWork.SaveChangesAsync();

        return response;
    }

    private async Task<Result<AuthTokensResponse>> GenerateTokenResponse(User user)
    {
        var generatedToken = await generateTokenService.GenerateAccessTokenAsync(user);
        var generateRefreshToken = generateTokenService.GenerateRefreshToken();

        if (!generatedToken.IsSuccess)
            return Result<AuthTokensResponse>.Failure(generatedToken.Error);
        
        return Result<AuthTokensResponse>.Success(new AuthTokensResponse(
            generatedToken.Value,
            generateRefreshToken.Value.Item1,
            generateRefreshToken.Value.Item2));
    }

    public async Task<Result> LogoutAsync(string refreshToken)
    {
        var tokenExists = await unitOfWork.RefreshTokenRepository.CheckTokenExists(refreshToken);
        if (!tokenExists)
            return Result.Failure(CommonErrors.InvalidRefreshToken());
        
        await unitOfWork.RefreshTokenRepository.RevokeToken(refreshToken);
        await unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
