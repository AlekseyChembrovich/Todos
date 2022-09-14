using Todos.Application.Common.Models;

namespace Todos.Application.Interfaces;

public interface IUserContextService
{
    User GetUser();
}
