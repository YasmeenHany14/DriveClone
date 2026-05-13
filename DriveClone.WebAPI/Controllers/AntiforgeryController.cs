using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

namespace DriveClone.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AntiforgeryController(
    IAntiforgery antiforgery) : ControllerBase
{
    [HttpGet]
    [IgnoreAntiforgeryToken]
    public IActionResult GetToken()
    {
        var tokenSet = antiforgery.GetAndStoreTokens(HttpContext);
        Response.Cookies.Append("XSRF-TOKEN", tokenSet.RequestToken!, new CookieOptions
        {
            Path = "/",
            HttpOnly = false
        });
        return Ok();
    }
}
