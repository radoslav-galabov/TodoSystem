using Microsoft.AspNetCore.Mvc;
using TodoSystem.Interfaces;
using TodoSystem.Models;

namespace TodoSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private readonly ITodoService _todoService;

        public TodoController(ILogger<TodoController> logger, ITodoService todoService)
        {
            _logger = logger;
            _todoService = todoService;
        }

        [HttpPost]
        public TodoItem Create([FromBody] TodoItem todo)
        {
            if (!ModelState.IsValid)
            {
                 BadRequest(ModelState);
            }

            var newTodo= _todoService.CreateTodo(todo);

            return newTodo;
        }

        [HttpPut]
        public TodoItem Edit([FromBody] TodoItem todo)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            var updatedTodo = _todoService.EditTodo(todo);

            return updatedTodo;
        }

        [HttpGet]
        public TodoView Get()
        {
            var todoView =  _todoService.GetTodos();

            return todoView;
        }

    }
}
