using Todos.AuthApi.Common.Models;
using Microsoft.AspNetCore.Identity;
using Todos.AuthApi.Common.Extensions;
using Todos.AuthApi.Services.Interfaces;

namespace Todos.AuthApi.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public IdentityService(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Result> LoginAsync(string userName, string password)
    {
        var signInResult = await _signInManager.PasswordSignInAsync(userName, password, false, false);

        return signInResult.ToResult();
    }

    public async Task<Result> RegisterAsync(string userName, string password)
    {
        var identityUser = new IdentityUser(userName);

        var creationResult = await _userManager.CreateAsync(identityUser, password);

        return creationResult.ToResult();
    }
}
