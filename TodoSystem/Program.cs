using TodoSystem;
using TodoSystem.Interfaces;
using TodoSystem.Services;
using Microsoft.EntityFrameworkCore;
using TodoSystem.Entities;
using TodoSystem.Enums;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ITodoService, TodoService>();

string connectionString = builder.Configuration.GetConnectionString("TodoConString");

builder.Services.AddDbContext<TodoContext>(x => x.UseInMemoryDatabase(connectionString));
builder.Services.AddScoped<TodoContext>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "TodoApi", Version = "v1" });
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

AddTodoData(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();

static void AddTodoData(WebApplication app)
{
    var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetService<TodoContext>();

    db.Todos.Add(new TodoEntity() { Id = 1, Name = "Todo 1", DueDate = DateTime.Now, Status = StatusEnum.Pending, DateCreated = DateTime.Now });
    db.Todos.Add(new TodoEntity() { Id = 2, Name = "Todo 2", DueDate = DateTime.Now, Status = StatusEnum.Pending, DateCreated = DateTime.Now });
    db.Todos.Add(new TodoEntity() { Id = 3, Name = "Todo 3", DueDate = DateTime.Now, Status = StatusEnum.Pending, DateCreated = DateTime.Now });
    db.Todos.Add(new TodoEntity() { Id = 4, Name = "Todo 4", DueDate = DateTime.Now, Status = StatusEnum.Overdue, DateCreated = DateTime.Now });
    db.Todos.Add(new TodoEntity() { Id = 5, Name = "Todo 5", DueDate = DateTime.Now, Status = StatusEnum.Overdue, DateCreated = DateTime.Now });
    db.Todos.Add(new TodoEntity() { Id = 6, Name = "Todo 6", DueDate = DateTime.Now, Status = StatusEnum.Overdue, DateCreated = DateTime.Now });
    db.Todos.Add(new TodoEntity() { Id = 7, Name = "Todo 7", DueDate = DateTime.Now, Status = StatusEnum.Done, DateCreated = DateTime.Now });
    db.Todos.Add(new TodoEntity() { Id = 8, Name = "Todo 8", DueDate = DateTime.Now, Status = StatusEnum.Done, DateCreated = DateTime.Now });

    db.SaveChanges();
}