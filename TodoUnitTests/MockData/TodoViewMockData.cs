using TodoSystem.Models;

namespace TodoUnitTests.MockData
{
    public class TodoViewMockData
    {
        public static TodoView GetView()
        {
            return new TodoView()
            {
                PendingTasks = new List<TodoItem>()
               {
                   new TodoItem() { Id = 1, Name = "Item 1", Status = TodoSystem.Enums.StatusEnum.Pending, Date = DateTime.Now },
                   new TodoItem() { Id = 2, Name = "Item 2", Status = TodoSystem.Enums.StatusEnum.Pending, Date = DateTime.Now }
               },
                OverdueTasks = new List<TodoItem>()
               {
                   new TodoItem() { Id = 1, Name = "Item 3", Status = TodoSystem.Enums.StatusEnum.Overdue, Date = DateTime.Now },
                   new TodoItem() { Id = 2, Name = "Item 4", Status = TodoSystem.Enums.StatusEnum.Overdue, Date = DateTime.Now }
               }
            };
        }
    }
}
