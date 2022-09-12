using MediatR;
using Todos.AuthApi.Common.Models;
using Todos.AuthApi.Services.Interfaces;

namespace Todos.AuthApi.Behaviors.Identity.Commands;

public class LoginIdentityCommand : IRequest<Result>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class LoginIdentityCommandHandler : IRequestHandler<LoginIdentityCommand, Result>
{
    private readonly IIdentityService _identityService;

    public LoginIdentityCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result> Handle(LoginIdentityCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.LoginAsync(request.UserName, request.Password);

        return result;
    }
}
