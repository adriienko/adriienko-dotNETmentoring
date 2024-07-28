using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.Db;

public class ToDoEFContext : DbContext
{
    public DbSet<ToDoItem> TodoItems { get; set; } = null!;

    public ToDoEFContext(DbContextOptions<ToDoEFContext> options)
        : base(options)
    {
    }

}

