using MediatR;
using Todos.AuthApi.Models;
using Microsoft.AspNetCore.Mvc;
using Todos.AuthApi.Behaviors.Identity.Commands;

namespace Todos.AuthApi.Controllers;

public class IdentityController : Controller
{
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        return View(new LoginUser
        {
            ReturnUrl = returnUrl
        });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginUser loginUser)
    {
        var result = await _mediator.Send(new LoginIdentityCommand
        {
            UserName = loginUser.UserName,
            Password = loginUser.Password
        });

        if (!result.IsSuccess)
        {
            return View(loginUser);
        }

        return Redirect(loginUser.ReturnUrl);
    }

    [HttpGet]
    public IActionResult Register(string returnUrl)
    {
        return View(new RegisterUser
        {
            ReturnUrl = returnUrl
        });
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUser registerUser)
    {
        var result = await _mediator.Send(new RegisterIdentityCommand
        {
            UserName = registerUser.UserName,
            Password = registerUser.Password
        });

        if (!result.IsSuccess)
        {
            return View(registerUser);
        }

        // await _signInManager.SignInAsync(user, false);
        return Redirect(registerUser.ReturnUrl);
    }

    //[HttpGet]
    //[Authorize]
    //public async Task<IActionResult> Logout(string logoutId)
    //{
    //    var res = HttpContext.User.Claims;

    //    await _signInManager.SignOutAsync();

    //    var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);

    //    if (string.IsNullOrEmpty(logoutRequest.PostLogoutRedirectUri))
    //    {
    //        return Redirect("http://localhost:4200");
    //    }

    //    return Redirect(logoutRequest.PostLogoutRedirectUri);
    //}
}
