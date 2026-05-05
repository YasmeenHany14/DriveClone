using DriveClone.Application.Common.ErrorAndResults;
using DriveClone.Application.DTOs.AuthDtos;
using DriveClone.Domain.Models;

namespace DriveClone.Application.Contracts;

public interface IAuthService
{
    Task<Result<User>> RegisterAsync(CreateUserAppDto registerDto);
    Task<Result<AuthTokensResponse>> LoginAsync(LoginDto userDto);
    Task<Result<AuthTokensResponse>> RefreshTokenAsync(string accessToken, string refreshToken);
    Task<Result> LogoutAsync(string refreshToken);
}