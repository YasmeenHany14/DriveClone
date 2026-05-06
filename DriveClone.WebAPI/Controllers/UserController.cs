using DriveClone.Application.Contracts;
using DriveClone.Application.DTOs.AuthDtos;
using DriveClone.WebAPI.Helpers.Extensions;
using DriveClone.WebAPI.Helpers.Filters;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriveClone.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(
    IUserService userService
    ) : ControllerBase
{
    [HttpPost(Name = "AddUser")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [InputValidationFilter<CreateUserAppDto>]
    public async Task<IActionResult> AddAsync([FromBody] CreateUserAppDto userAppDto)
    {
        var result = await userService.AddAsync(userAppDto);
        if (!result.IsSuccess)
            return result.ToActionResult();
        return Created();
    }
    
    [HttpGet("{id}", Name = "GetUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [RequireAntiforgeryToken]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ServiceFilter<CanAccessResourceFilter>]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        var result = await userService.GetByIdAsync(id);
        return result.ToActionResult();
    }

    [HttpDelete("{id}", Name = "DeleteUser")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [RequireAntiforgeryToken]
    [ServiceFilter<CanAccessResourceFilter>]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        var result = await userService.DeleteAsync(id);
        return result.ToActionResult();
    }

}
