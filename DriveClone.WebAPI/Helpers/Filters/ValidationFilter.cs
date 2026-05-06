using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DriveClone.WebAPI.Helpers.Filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        //
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
            return;
        
        var problem = new ValidationProblemDetails(context.ModelState)
        {
            Type = "https://httpstatuses.com/400",
            Title = "One or more validation errors occurred.",
            Status = StatusCodes.Status400BadRequest,
        };
        // context.Result = new ObjectResult(problem);
        context.Result = new BadRequestObjectResult(problem);
    }
}
