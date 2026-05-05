using DriveClone.Application.Common.ErrorAndResults;
using DriveClone.Application.DTOs.AuthDtos;

namespace DriveClone.Application.Contracts;

public interface IUserService
{
    Task<Result<string>> AddAsync(CreateUserAppDto createUserDto);
}