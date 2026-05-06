using DriveClone.Application.Common.Constants.Errors;
using DriveClone.Application.Common.ErrorAndResults;
using Microsoft.AspNetCore.Mvc;

namespace DriveClone.WebAPI.Helpers.Extensions;

public static class ResultExtension
{
    public static IActionResult ToActionResult(this Result result, string? instance = null)
    {
        if (result.IsSuccess)
            return new OkResult();

        return result.Error.MapErrorResult(instance);
    }

    public static IActionResult ToActionResult<T>(this Result<T> result, string? instance = null)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Value);
        return result.Error.MapErrorResult(instance);
    }

    private static IActionResult MapErrorResult(this Error error, string? instance = null)
    {
        var (status, title, type) = error.Code switch
        {
            nameof(CommonErrors.NotFound)
                or "NotFound"
                => (404, "Resource not found.", "https://tools.ietf.org/html/rfc9110#section-15.5.5"),

            nameof(CommonErrors.Unauthorized)
                or "Unauthorized"
                or nameof(CommonErrors.WrongCredentials)
                or "WrongCredentials"
                or nameof(CommonErrors.InvalidRefreshToken)
                or "InvalidRefreshToken"
                => (401, "Unauthorized.", "https://tools.ietf.org/html/rfc9110#section-15.5.2"),

            nameof(AuthErrors.Forbidden)
                or "Forbidden"
                => (403, "Forbidden.", "https://tools.ietf.org/html/rfc9110#section-15.5.4"),

            nameof(CommonErrors.InvalidInput)
                or "InvalidInput"
                => (400, "Invalid input.", "https://tools.ietf.org/html/rfc9110#section-15.5.1"),

            nameof(CommonErrors.ValidationProblem)
                or "ValidationProblem"
                => (400, "One or more validation errors occurred.",
                    "https://tools.ietf.org/html/rfc9110#section-15.5.1"),

            nameof(CommonErrors.InternalServerError)
                or "InternalServerError"
                or nameof(CommonErrors.CannotGenerateToken)
                or "CannotGenerateToken"
                => (500, "An unexpected error occurred.", "https://tools.ietf.org/html/rfc9110#section-15.6.1"),

            _ => (500, "An unexpected error occurred.", "https://tools.ietf.org/html/rfc9110#section-15.6.1")
        };

        if (error.Errors is not null)
        {
            var validationProblem = new ValidationProblemDetails(error.Errors)
            {
                Type = type,
                Title = title,
                Status = status,
                Detail = error.Description,
                Instance = instance
            };
            return new ObjectResult(validationProblem) {StatusCode =  status};
        }
        var problem = new ProblemDetails
        {
            Type     = type,
            Title    = title,
            Status   = status,
            // Detail   = error.Description,
            Instance = instance
        };
        return new ObjectResult(problem) { StatusCode = status };
    }
}
