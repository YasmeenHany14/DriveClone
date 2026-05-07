using DriveClone.Application.Contracts;
using DriveClone.Application.DTOs.AuthDtos;
using DriveClone.Application.DTOs.UserDtos;
using DriveClone.Application.Helpers;
using DriveClone.Application.Validators.UserValidators;
using DriveClone.WebAPI.Helpers.Extensions;
using DriveClone.WebAPI.Helpers.Filters;
using DriveClone.WebAPI.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DriveClone.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(
    IUserService userService,
    IValidator<UpdateUserAppDto> updateUserValidator
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

    [HttpPatch("{id}", Name = "UpdateUser")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // [RequireAntiforgeryToken]
    [ServiceFilter<CanAccessResourceFilter>]
    public async Task<IActionResult> UpdateAsync(
        string id, JsonPatchDocument<UpdateUserAppDto> patchDocument)
    {
        var userResult = await userService.GetByIdAsync(id);
        if (!userResult.IsSuccess)
            return userResult.ToActionResult();

        var userToPatch = userResult.Value;
        var (patchedDto, validationResult) = this.HandlePatch(userToPatch, patchDocument);
        if (!validationResult.IsSuccess)
            return validationResult.ToActionResult();

        var inputValid = await ValidationHelper.ValidateAndReportAsync(updateUserValidator, patchedDto, "Input");
        if (!inputValid.IsSuccess)
            return inputValid.ToActionResult();

        var result = await userService.UpdateAsync(id, patchedDto);
        if (!result.IsSuccess)
            return result.ToActionResult();

        return NoContent();
    }

    [HttpPost("{id}/change-password", Name = "ChangePassword")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [InputValidationFilter<UpdatePasswordRequestAppDto>]
    // [RequireAntiforgeryToken]
    [ServiceFilter<CanAccessResourceFilter>]
    public async Task<IActionResult> ChangePasswordAsync(string id,
        UpdatePasswordRequestAppDto passwordDto)
    {
        var result = await userService.ChangePasswordAsync(id, passwordDto);
        return result.ToActionResult();
    }
}
