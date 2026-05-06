using AutoMapper;
using DriveClone.Application.Common.Constants.Errors;
using DriveClone.Application.Common.ErrorAndResults;
using DriveClone.Application.Contracts;
using DriveClone.Application.DTOs.AuthDtos;
using DriveClone.Application.DTOs.UserDtos;
using DriveClone.Application.Helpers;
using DriveClone.Domain.Models;
using DriveClone.Domain.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace DriveClone.Application.Services;

public class UserService(
    IUnitOfWork unitOfWork,
    IAuthService authService,
    UserManager<User> userManager,
    IMapper mapper,
    IValidator<CreateUserAppDto> createUserValidator) : IUserService
{
    public async Task<Result<string>> AddAsync(CreateUserAppDto createUserDto)
    {
        var validationResult = await ValidationHelper.ValidateAndReportAsync(createUserValidator, createUserDto, "CreateBusiness");
        if(!validationResult.IsSuccess)
            return Result<string>.Failure(validationResult.Error);
        
        var createUserResult = await authService.RegisterAsync(createUserDto);
        if (!createUserResult.IsSuccess)
            return Result<string>.Failure(createUserResult.Error);
        
        return Result<string>.Success(createUserResult.Value.Id);
    }
    
    public async Task<Result<GetUserByIdAppDto>> GetByIdAsync(string Id)
    {
        var user = await userManager.FindByIdAsync(Id);
        if (user == null || !user.IsActive)
            return Result<GetUserByIdAppDto>.Failure(CommonErrors.NotFound());
        
        var mappedUser = mapper.Map<GetUserByIdAppDto>(user);
        return Result<GetUserByIdAppDto>.Success(mappedUser);
    }

    public async Task<Result> DeleteAsync(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user == null || !user.IsActive)
            return Result.Failure(CommonErrors.NotFound());

        user.IsActive = false;
        var identityResult = await userManager.UpdateAsync(user);

        return identityResult.Succeeded ? Result.Success() : Result.Failure(CommonErrors.InternalServerError(ErrorMsgs.CannotDeleteUser));
    }
}
