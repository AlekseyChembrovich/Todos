using MediatR;
using Todos.AuthApi.Common.Models;
using Todos.AuthApi.Services.Interfaces;

namespace Todos.AuthApi.Behaviors.Identity.Commands;

public class RegisterIdentityCommand : IRequest<Result>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class RegisterIdentityCommandHandler : IRequestHandler<RegisterIdentityCommand, Result>
{
    private readonly IIdentityService _identityService;

    public RegisterIdentityCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result> Handle(RegisterIdentityCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.RegisterAsync(request.UserName, request.Password);

        return result;
    }
}
