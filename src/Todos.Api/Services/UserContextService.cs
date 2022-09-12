using System.Security.Claims;
using Todos.Application.Interfaces;

namespace Todos.Api.Services;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _accessor;

    public UserContextService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public string GetUserId()
    {
        var user = _accessor.HttpContext.User;

        var userIdClaim = user.Claims.Where(claim => claim.Type.Equals(ClaimTypes.NameIdentifier)).FirstOrDefault();

        return userIdClaim?.Value;
    }
}
