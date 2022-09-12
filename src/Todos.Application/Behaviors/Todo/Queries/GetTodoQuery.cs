using MediatR;
using Todos.Application.Common.Dto;
using Todos.Application.Interfaces;

namespace Todos.Application.Behaviors.Todo.Queries;

public class GetTodoQuery : IRequest<TodoDto>
{
    public string Id { get; set; }
}

public class GetAllTodoQueryHandler : IRequestHandler<GetTodoQuery, TodoDto>
{
    private readonly ITodoService _todoService;

    public GetAllTodoQueryHandler(ITodoService todoService)
    {
        _todoService = todoService;
    }

    public async Task<TodoDto> Handle(GetTodoQuery request, CancellationToken cancellationToken)
    {
        var todoItem = await _todoService.GetTodoAsync(request.Id);
        var todoDto = new TodoDto
        {
            Id = todoItem.Id,
            Task = todoItem.Task,
            CreatedDate = todoItem.CreatedDate,
            ExpirationDate = todoItem.ExpirationDate
        };

        return todoDto;
    }
}
