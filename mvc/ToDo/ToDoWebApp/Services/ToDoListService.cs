using Microsoft.EntityFrameworkCore;
using ToDo.Db;
using ToDo.Models;

namespace ToDo.Services;
public class ToDoListService : IToDoListService
{

    
    private readonly string _notFoudMessage = "item not found";

    private readonly Dictionary<Guid, string> Storage = new Dictionary<Guid, string>();
    private readonly TodoDbContext _context;

    public ToDoListService(TodoDbContext context)
    {
        _context = context;
    }

    public List<ToDoItem> GetItemList()
    {
        var list = _context.TodoItems.ToList();
        return list;
    }

    public Guid Create(ToDoItem item)
    {
        item.Id = Guid.NewGuid();
        _context.TodoItems.Add(item);
        _context.SaveChanges();
        return item.Id;
    }

    public ToDoItem? Read(Guid id)
    {
        var item = _context.TodoItems.FirstOrDefault(x => x.Id == id);
        return item;
    }

    public void Delete(Guid id)
    {
        var item = Read(id);
        if (item != null)
        {
            _context.TodoItems.Remove(item);
        }
        _context.SaveChanges();
    }

    public void Update(ToDoItem item)
    {

        _context.TodoItems.Attach(item);
        _context.Entry(item).State = EntityState.Modified;

        _context.SaveChanges();
    }
}
