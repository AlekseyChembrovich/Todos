using System.Security.Claims;
using Todos.Application.Common.Models;
using Todos.Application.Common.Interfaces;

namespace Todos.Api.Services;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _accessor;

    public UserContextService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public User GetUser()
    {
        var user = _accessor.HttpContext.User;
        var userIdClaim = user.Claims.Where(claim => claim.Type.Equals(ClaimTypes.NameIdentifier)).FirstOrDefault();
        var userNameClaim = user.Claims.Where(claim => claim.Type.Equals(ClaimTypes.Name)).FirstOrDefault();

        return new User
        {
            Id = userIdClaim?.Value,
            Name = userNameClaim?.Value,
        };
    }
}
