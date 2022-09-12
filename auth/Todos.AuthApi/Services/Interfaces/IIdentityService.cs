using Todos.AuthApi.Common.Models;

namespace Todos.AuthApi.Services.Interfaces;

public interface IIdentityService
{
    Task<Result> LoginAsync(string userName, string password);

    Task<Result> RegisterAsync(string userName, string password);
}
