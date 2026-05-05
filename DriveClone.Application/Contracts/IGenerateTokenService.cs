using System.Security.Claims;
using DriveClone.Application.Common.ErrorAndResults;
using DriveClone.Domain.Models;

namespace DriveClone.Application.Contracts;

public interface IGenerateTokenService
{
    Task<Result<string>> GenerateAccessTokenAsync(User user);
    Result<(string, DateTime)> GenerateRefreshToken();
    Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
}
