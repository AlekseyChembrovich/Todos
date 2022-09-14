using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace Todos.AuthApi.Controllers;

[ApiController]
[Route("api/info")]
public class InfoController : Controller
{
    private UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;

    public InfoController(UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpGet("users")]
    public IActionResult GetUsers()
    {
        var compressedUsers = _userManager.Users.Select(user => new
        {
            Id = user.Id,
            Name = user.UserName,
            Email = user.Email
        }).ToList();

        return Ok(compressedUsers);
    }

    [HttpGet("auth")]
    public IActionResult GetAuthInfo()
    {
        var authConfig = new
        {
            Client = _configuration.GetValue<string>("AuthConfig:Client")
        };

        return Ok(authConfig);
    }
}
