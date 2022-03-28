using Domain.Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase {
    private ITodoHome todoHome;

    public TodoController(ITodoHome todoHome) {
        this.todoHome = todoHome;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Todo>>> GetAll([FromQuery] int? OwnerId, [FromQuery] bool? isCompleted) {
        try {
            ICollection<Todo> todos = await todoHome.GetAsync();
            if (OwnerId == null && isCompleted == null) {
                return Ok(todos);
            }

            if (OwnerId == null) {
                return Ok(todos.Where(todo => todo.IsCompleted == isCompleted));
            }

            if (isCompleted==null) {
                return Ok(todos.Where(todo => todo.OwnerId == OwnerId));
            }

            return Ok(todos.Where(todo => todo.IsCompleted == isCompleted && todo.OwnerId == OwnerId));
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Todo>> AddTodo([FromBody] Todo todo) {
        try {
            Todo added = await todoHome.AddAsync(todo);
            return Created($"/todos/{added.Id}", added);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    [Route("/{id:int}")]
    public async Task<ActionResult<Todo>> GetById([FromRoute] int id) {
        try {
            Todo fromServer = await todoHome.GetById(id);
            return Ok(fromServer);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult<Todo>> Update([FromBody] Todo todo) {
        try {
            await todoHome.UpdateAsync(todo);
            Todo todoUpdated = await todoHome.GetById(todo.Id);
            return Ok(todoUpdated);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }
}