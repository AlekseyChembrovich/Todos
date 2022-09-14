using MediatR;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Todos.Application.Behaviors.Todo.Queries;
using Todos.Application.Behaviors.Todo.Commands;

namespace Todos.Api.Controllers;

[ApiController]
[Route("api/todos")]
[Authorize(Roles = "User")]
public class TodoController : Controller
{
    private readonly IMediator _mediator;

    public TodoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        Log.Information("Get user list of todos.");

        var todos = await _mediator.Send(new GetTodosQuery());

        return Ok(todos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodos(string id)
    {
        Log.Information("Get user todo.");

        var todo = await _mediator.Send(new GetTodoQuery
        {
            Id = id
        });

        return todo is not null ? Ok(todo) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodo(CreateTodoCommand createTodoCommand)
    {
        Log.Information($"Create user todo (task: {createTodoCommand.Task}).");

        var todoDto = await _mediator.Send(createTodoCommand);

        return Created($"/todos/{todoDto.Id}", todoDto);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTodo(UpdateTodoCommand updateTodoCommand)
    {
        Log.Information($"Update todo (todo id: {updateTodoCommand.Id}).");

        await _mediator.Send(updateTodoCommand);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveTodo(string id)
    {
        Log.Information($"Remove todo (todo id: {id}).");

        await _mediator.Send(new RemoveTodoCommand(id));

        return NoContent();
    }
}
