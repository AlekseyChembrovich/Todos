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
        var user = _userContext.GetUser();
        var todoItem = new TodoItem
        {
            Task = request.Task,
            ExpirationDate = request.ExpirationDate,
            UserId = user.Id
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
