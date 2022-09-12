using MediatR;
using Todos.Application.Interfaces;

namespace Todos.Application.Behaviors.Todo.Commands;

public record RemoveTodoCommand(string Id) : IRequest;

public class RemoveExistTodoCommandHandler : IRequestHandler<RemoveTodoCommand>
{
    private readonly ITodoService _todoService;

    public RemoveExistTodoCommandHandler(ITodoService todoService)
    {
        _todoService = todoService;
    }

    public async Task<Unit> Handle(RemoveTodoCommand request, CancellationToken cancellationToken)
    {
        await _todoService.RemoveTodoAsync(request.Id);

        return Unit.Value;
    }
}
