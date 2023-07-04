using Microsoft.AspNetCore.Mvc;

namespace Todos.Api.Controllers;

[ApiController]
[Route("api/info")]
public class InfoController : Controller
{
    private readonly IConfiguration _configuration;

    public InfoController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("postgres")]
    public IActionResult GetPostgresDbInfo()
    {
        var postgresConnection = _configuration.GetConnectionString("ResourceDbConnectionString");

        return Ok(postgresConnection);
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
