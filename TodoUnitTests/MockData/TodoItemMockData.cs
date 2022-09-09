using TodoSystem.Models;

namespace TodoUnitTests.MockData
{
    public class TodoItemMockData
    {
        public static TodoItem GetNewItem()
        {
            return new TodoItem() { Id = 1, Name = "Item 1", Status = TodoSystem.Enums.StatusEnum.Pending, Date = DateTime.Now };
        }
    }
}
