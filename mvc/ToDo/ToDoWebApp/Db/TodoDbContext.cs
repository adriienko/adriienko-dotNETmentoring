using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.Db;

public class TodoDbContext : DbContext
{
    public DbSet<ToDoItem> TodoItems { get; set; } = null!;

    public TodoDbContext(DbContextOptions<TodoDbContext> options)
        : base(options)
    {
    }

}
