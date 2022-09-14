using Serilog.Context;
using Todos.Application.Interfaces;

namespace Todos.Api.Middleware;

public class LogEnrichMiddleware
{
    private readonly RequestDelegate next;
    private readonly IUserContextService _userContext;

    public LogEnrichMiddleware(RequestDelegate next, IUserContextService userContext)
    {
        this.next = next;
        _userContext = userContext;
    }

    public Task Invoke(HttpContext context)
    {
        var user = _userContext.GetUser();
        LogContext.PushProperty("UserId", user.Id);
        LogContext.PushProperty("UserName", user.Name);

        return next(context);
    }
}
