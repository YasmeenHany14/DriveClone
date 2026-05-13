using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DriveClone.WebAPI.Helpers.Filters;

public class ValidateAntiForgeryTokenFilter : IAsyncActionFilter
{
    private static readonly HashSet<string> SafeMethods = new(StringComparer.OrdinalIgnoreCase)
    {
        "GET", "HEAD", "OPTIONS", "TRACE"
    };

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!SafeMethods.Contains(context.HttpContext.Request.Method))
        {
            try
            {
                var antiForgeryService = context.HttpContext.RequestServices.GetRequiredService<IAntiforgery>();
                await antiForgeryService.ValidateRequestAsync(context.HttpContext);
            }
            catch (AntiforgeryValidationException)
            {
                //TODO: MAKE IT PROBLEM DETAILS FORMAT LATER
                context.Result = new BadRequestObjectResult("Antiforgery validation failed");
                return;
            }
        }
        await next();
    }
}
