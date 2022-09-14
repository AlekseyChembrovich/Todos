using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Todos.Infrastructure.Common.Models;

namespace Todos.Api.Controllers;

[ApiController]
[Route("api/info")]
public class InfoController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly TodosDatabaseSettings _todosDatabaseSettings;

    public InfoController(
        IConfiguration configuration,
        IOptions<TodosDatabaseSettings> todosDatabaseSettings)
    {
        _configuration = configuration;
        _todosDatabaseSettings = todosDatabaseSettings.Value;
    }

    [HttpGet("mongo")]
    public IActionResult GetMongoDbInfo()
    {
        return Ok(_todosDatabaseSettings);
    }

    [HttpGet("auth")]
    public IActionResult GetAuthInfo()
    {
        var authConfig = new
        {
            Authority = _configuration.GetValue<string>("AuthConfig:Authority")
        };

        return Ok(authConfig);
    }
}
