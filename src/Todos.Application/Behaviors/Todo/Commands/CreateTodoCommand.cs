using MediatR;
using Todos.Domain.Entities.Todo;
using Todos.Application.Interfaces;
using Todos.Application.Common.Dto;

namespace Todos.Application.Behaviors.Todo.Commands;

public class CreateTodoCommand : IRequest<TodoDto>
{
    public string Task { get; set; }
    public DateTime ExpirationDate { get; set; }
}

public class CreateNewTodoCommandHandler : IRequestHandler<CreateTodoCommand, TodoDto>
{
    private readonly ITodoService _todoService;
    private readonly IUserContextService _userContext;

    public CreateNewTodoCommandHandler(ITodoService todoService, IUserContextService userContext)
    {
        _todoService = todoService;
        _userContext = userContext;
    }

    public async Task<TodoDto> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();
        var todoItem = new TodoItem
        {
            Task = request.Task,
            ExpirationDate = request.ExpirationDate,
            UserId = userId
        };

        var createdTodoItem = await _todoService.CreateTodoAsync(todoItem);
        var todoDto = new TodoDto
        {
            Id = createdTodoItem.Id,
            Task = createdTodoItem.Task,
            CreatedDate = createdTodoItem.CreatedDate,
            ExpirationDate = createdTodoItem.ExpirationDate
        };

        return todoDto;
    }
}
