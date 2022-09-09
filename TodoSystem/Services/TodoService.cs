using Microsoft.EntityFrameworkCore;
using TodoSystem.Entities;
using TodoSystem.Interfaces;
using TodoSystem.Models;

namespace TodoSystem.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoContext _context;
        public TodoService(TodoContext context)
        {
            _context = context;
        }
        public TodoItem CreateTodo(TodoItem todo)
        {
            TodoEntity todoEntity = new TodoEntity();
            todoEntity.Name = todo.Name;
            todoEntity.DueDate = todo.Date;
            todoEntity.Status = todo.Status;
            todoEntity.DateCreated = DateTime.Now;

            _context.Todos.Add(todoEntity);

            _context.SaveChanges();

            var newTodo = _context.Todos.OrderByDescending( t => t.DateCreated ).FirstOrDefault();

            if (newTodo != null)
            {
                todo.Id = newTodo.Id;
            }

            return todo;
        }

        public TodoItem EditTodo(TodoItem item)
        {
            var todo = _context.Todos.FirstOrDefault(t => t.Id == item.Id);
            if (todo != null)
            {
                todo.Name = item.Name;
                todo.DueDate = item.Date;
                todo.Status = item.Status;

                _context.SaveChanges();
            }

            return item;
        }

        public TodoView GetTodos()
        {
            TodoView todoView = new TodoView();

            todoView.PendingTasks =  _context.Todos
                .Where(t => (t.Status != Enums.StatusEnum.Done) && (t.Status == Enums.StatusEnum.Pending))
                .Select(c => new TodoItem() 
                { 
                    Id = c.Id, 
                    Name = c.Name,
                    Date = c.DueDate,
                    Status = c.Status }) 
                .ToList();

            todoView.OverdueTasks =  _context.Todos
                .Where(t => (t.Status != Enums.StatusEnum.Done) && (t.Status == Enums.StatusEnum.Overdue))
                .Select(c => new TodoItem()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Date = c.DueDate,
                    Status = c.Status
                })
                .ToList();

            return todoView;
        }
    }
}
