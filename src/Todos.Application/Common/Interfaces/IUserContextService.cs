using Todos.Application.Common.Models;

namespace Todos.Application.Common.Interfaces;

public interface IUserContextService
{
    User GetUser();
}
