using DriveClone.Application.Common.ErrorAndResults;
using DriveClone.Application.Contracts;
using DriveClone.Application.DTOs.AuthDtos;
using DriveClone.Application.Helpers;
using DriveClone.Domain.Repositories;
using FluentValidation;

namespace DriveClone.Application.Services;

public class UserService(
    IUnitOfWork unitOfWork,
    IAuthService authService,
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
}
