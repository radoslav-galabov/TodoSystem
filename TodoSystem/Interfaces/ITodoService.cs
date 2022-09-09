using TodoSystem.Models;

namespace TodoSystem.Interfaces
{
    public interface ITodoService
    {
        TodoItem CreateTodo(TodoItem item);
        TodoItem EditTodo(TodoItem item);
        TodoView GetTodos();
    }
}
