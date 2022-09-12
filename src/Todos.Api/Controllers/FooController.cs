using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Todos.Api.Controllers;

[ApiController]
[Route("api/foo")]
public class FooController : Controller
{
    [HttpGet("user")]
    [Authorize(Roles = "User")]
    public IActionResult FooUser()
    {
        return Ok("Hello some user!");
    }

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public IActionResult FooAdmin()
    {
        return Ok("Hello some admin!");
    }
}
