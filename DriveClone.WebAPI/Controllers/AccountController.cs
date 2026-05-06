using DriveClone.Application.Contracts;
using DriveClone.Application.DTOs.AuthDtos;
using DriveClone.WebAPI.Helpers.Extensions;
using DriveClone.WebAPI.Helpers.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriveClone.WebAPI.Controllers;

[ApiController ]
[Route("api/[controller]")]
public class AccountController(
    IAuthService authService
    ) : ControllerBase
{
    // Login
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        var response = await authService.LoginAsync(model);
        return response.ToActionResult();
    }
    
    // Logout
    [HttpPost("logout")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest logoutRequest)
    {
        var result = await authService.LogoutAsync(logoutRequest.RefreshToken);
        return result.ToActionResult();
    }
    
    // Refresh
    [HttpPost("refresh-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [InputValidationFilter<RefreshRequestDto>]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshRequestDto refreshRequest)
    {
        var response = await authService.RefreshTokenAsync(refreshRequest.AccessToken, refreshRequest.RefreshToken);
        return response.ToActionResult();
    }
}
