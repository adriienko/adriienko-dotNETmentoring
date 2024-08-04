using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using ToDo.Db;
using ToDo.Models;


namespace ToDo.Services;

public class ToDoEfDbListService : IToDoListSearchableService
{
    private readonly ToDoEFContext _context;

    public ToDoEfDbListService(ToDoEFContext context)
    {
        _context = context;
    }

    public List<ToDoItem> FindInTitle(string s)
    {
        var list = _context.TodoItems.Where(i => i.Title.Contains(s)).ToList();
        return list;
    }

    public List<ToDoItem> GetPagedItemList(int size, int page)
    {
        var skip = (page - 1) * size;
        var list = _context.TodoItems.Skip(skip).Take(size).ToList();
        return list;
    }

    public List<ToDoItem> GetItemList()
    {
        var list = _context.TodoItems.ToList();
        return list;
    }

    public Guid Create(ToDoItem item)
    {
        var id = Guid.NewGuid();
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
        var entity = Read(item.Id);

        entity.Title = item.Title;
        entity.IsCompleted = item.IsCompleted;

        _context.SaveChanges();
    }
}
