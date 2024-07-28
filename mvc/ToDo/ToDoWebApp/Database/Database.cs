using Microsoft.EntityFrameworkCore;
using ToDo.Db;

namespace ToDo;

internal class Database
{
    public static void MigrateDatabase(WebApplication app)
    {
        using (var container = app.Services.CreateScope())
        {
            var dbContext = container.ServiceProvider.GetService<ToDoEFContext>();
            var pendingMigration = dbContext!.Database.GetPendingMigrations();
            if (pendingMigration.Any())
            {
                Console.WriteLine("Add migrations...");
                dbContext.Database.Migrate();
            }
        }
    }
}
