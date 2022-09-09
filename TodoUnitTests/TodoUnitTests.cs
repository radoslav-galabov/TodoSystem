using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using TodoSystem;
using TodoSystem.Controllers;
using TodoSystem.Interfaces;
using TodoSystem.Services;
using TodoUnitTests.MockData;

namespace TodoUnitTests
{
    public class TodoUnitTests
    {
        protected readonly TodoContext _context;
        public TodoUnitTests()
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            _context = new TodoContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public void Get_All_Todos_Should_Return_All_Todo_Items()
        {
            /// Arrange
            var todoService = new Mock<ITodoService>();
            todoService.Setup(_ => _.GetTodos()).Returns(TodoViewMockData.GetView());

            var ctr = new TodoController(null, todoService.Object);

            /// Act
            var result = ctr.Get();


            // Assert
            result.OverdueTasks[0].Name.Should().Be("Item 3");
            result.OverdueTasks.Count.Should().Be(2);
        }

        [Fact]
        public void Create_Todo_Should_Return_New_Todo()
        {
            /// Arrange
            var todoService = new Mock<ITodoService>();
            var newTodo = TodoItemMockData.GetNewItem();
            var ctr = new TodoController(null, todoService.Object);

            /// Act
            var result =  ctr.Create(newTodo);

            /// Assert
            todoService.Verify(_ => _.CreateTodo(newTodo), Times.Exactly(1));
        }

        [Fact]
        public async Task SaveAsync_AddNewTodo()
        {
            /// Arrange
            int existingRecordCount = _context.Todos.Count();
            int newRecordCount = TodoEntityMock.GetEntities().Count();

            var newTodo = TodoItemMockData.GetNewItem();

            _context.Todos.AddRange(TodoEntityMock.GetEntities());
            _context.SaveChanges();

            var sut = new TodoService(_context);

            /// Act
            sut.CreateTodo(newTodo);

            ///Assert
            _context.Todos.Count().Should().Be(newRecordCount + existingRecordCount + 1);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}