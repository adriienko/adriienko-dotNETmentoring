using Microsoft.EntityFrameworkCore;
using ToDo;
using ToDo.Db;
using ToDo.Models;
using ToDo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IToDoListService, ToDoEfDbListService>();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ToDoEFContext>(option =>
    option.UseSqlServer(connectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//}
//app.UseStaticFiles();


app.UseRouting();

//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

Database.MigrateDatabase(app);

app.Run();
