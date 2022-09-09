namespace TodoSystem.Models
{
    public class TodoView
    {
        public IList<TodoItem> PendingTasks { get; set; }
        public IList<TodoItem> OverdueTasks { get; set; }
    }
}
