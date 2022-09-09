using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;
using TodoSystem.Entities;

namespace TodoSystem
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) 
        {
          
        }

        public DbSet<TodoEntity> Todos { get; set; }
    }
}
