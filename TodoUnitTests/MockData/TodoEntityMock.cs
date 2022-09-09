using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoSystem.Entities;
using TodoSystem.Models;

namespace TodoUnitTests.MockData
{
    public class TodoEntityMock
    {


        public static List<TodoEntity> GetEntities()
        {
            return new List<TodoEntity>()
            {
                new TodoEntity() { Id = 12, Name = "Item 10", Status = TodoSystem.Enums.StatusEnum.Pending, DueDate = DateTime.Now },
                new TodoEntity() { Id = 13, Name = "Item 20", Status = TodoSystem.Enums.StatusEnum.Overdue, DueDate = DateTime.Now }
            };
        }
    }
}
