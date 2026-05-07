using DriveClone.Application.Common.Constants.Errors;
using DriveClone.Application.Common.ErrorAndResults;
using DriveClone.Application.DTOs;
using DriveClone.Application.Mappers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DriveClone.WebAPI.Helpers;

public static class PatchHelper
{
    public static (TUpdateDto PatchedDto, Result ValidationResult) HandlePatch<TGetDto, TUpdateDto>(
        this ControllerBase controller,
        TGetDto originalDto,
        JsonPatchDocument<TUpdateDto> patchDoc)
        where TGetDto : AppBaseDto
        where TUpdateDto : AppBaseDto, new ()
    {
        var updateDto = originalDto.MapTo<TGetDto, TUpdateDto>();
        patchDoc.ApplyTo(updateDto, controller.ModelState);
        if (!controller.ModelState.IsValid)
        {
            return (updateDto, Result.Failure(CommonErrors.PatchValidationProblem()));
        }
        return (updateDto, Result.Success());
    }
}