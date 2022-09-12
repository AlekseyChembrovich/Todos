using MediatR;
using Todos.Domain.Entities.Todo;
using Todos.Application.Interfaces;

namespace Todos.Application.Behaviors.Todo.Commands;

public class UpdateTodoCommand : IRequest
{
    public string Id { get; set; }
    public string Task { get; set; }
    public DateTime ExpirationDate { get; set; }
}

public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand>
{
    private readonly ITodoService _todoService;

    public UpdateTodoCommandHandler(ITodoService todoService)
    {
        _todoService = todoService;
    }

    public async Task<Unit> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var todoItem = new TodoItem
        {
            Id = request.Id,
            Task = request.Task,
            ExpirationDate = request.ExpirationDate
        };

        await _todoService.UpdateTodoAsync(todoItem);

        return Unit.Value;
    }
}
