using DriveClone.Application.Contracts;
using DriveClone.Application.DTOs.AuthDtos;
using DriveClone.WebAPI.Helpers.Extensions;
using DriveClone.WebAPI.Helpers.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DriveClone.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(
    IUserService userService
    ) : ControllerBase
{
    [HttpPost(Name = "AddStudent")]
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
}
