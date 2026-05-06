using DriveClone.Application.Common.ErrorAndResults;
using DriveClone.Application.DTOs.AuthDtos;
using DriveClone.Application.DTOs.UserDtos;

namespace DriveClone.Application.Contracts;

public interface IUserService
{
    Task<Result<string>> AddAsync(CreateUserAppDto createUserDto);
    Task<Result<GetUserByIdAppDto>> GetByIdAsync(string id);
    Task<Result> DeleteAsync(string id);
}