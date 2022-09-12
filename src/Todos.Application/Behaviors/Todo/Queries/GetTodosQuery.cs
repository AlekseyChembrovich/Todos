using MediatR;
using Todos.Application.Common.Dto;
using Todos.Application.Interfaces;

namespace Todos.Application.Behaviors.Todo.Queries;

public class GetTodosQuery : IRequest<IEnumerable<TodoDto>> { }

public class GetAllTodosQueryHandler : IRequestHandler<GetTodosQuery, IEnumerable<TodoDto>>
{
    private readonly ITodoService _todoService;
    private readonly IUserContextService _userContext;

    public GetAllTodosQueryHandler(ITodoService todoService, IUserContextService userContext)
    {
        _todoService = todoService;
        _userContext = userContext;
    }

    public async Task<IEnumerable<TodoDto>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();
        var todoItems = await _todoService.GetTodosAsync(userId);

        var todoDtos = todoItems.Select(todoItem => new TodoDto
        {
            Id = todoItem.Id,
            Task = todoItem.Task,
            CreatedDate = todoItem.CreatedDate,
            ExpirationDate = todoItem.ExpirationDate
        });

        return todoDtos;
    }
}
