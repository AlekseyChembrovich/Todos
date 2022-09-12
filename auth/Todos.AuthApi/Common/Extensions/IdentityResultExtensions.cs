using Todos.AuthApi.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Todos.AuthApi.Common.Extensions;

public static class IdentityResultExtensions
{
    public static Result ToResult(this SignInResult signInResult)
    {
        return signInResult.Succeeded
            ? Result.Success()
            : Result.Fail(null);
    }

    public static Result ToResult(this IdentityResult identityResult)
    {
        return identityResult.Succeeded
            ? Result.Success()
            : Result.Fail(identityResult.Errors.Select(err => err.Description));
    }
}
