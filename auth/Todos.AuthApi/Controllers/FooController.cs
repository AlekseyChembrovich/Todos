using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Todos.AuthApi.Controllers;

[ApiController]
[Route("api/foo")]
public class FooController : Controller
{
    private UserManager<IdentityUser> _userManager;

    public FooController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("users")]
    public IActionResult GetUsers()
    {
        var compressedUsers = _userManager.Users.Select(user => new
        {
            Id = user.Id,
            Name = user.UserName,
            Email = user.Email
        })
            .ToList();

        return Ok(compressedUsers);
    }

    [HttpGet]
    public IActionResult Foo()
    {
        return Ok("Hello!");
    }
}
