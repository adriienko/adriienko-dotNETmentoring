
using ToDo.Models;

namespace ToDo.Services
{
    public interface IToDoListService
    {
        Guid Create(ToDoItem item);
        ToDoItem Read(Guid id);        
        void Update(ToDoItem item);
        void Delete(Guid id);
        List<ToDoItem> GetItemList();
    }

    public interface IToDoListSearchableService : IToDoListService
    {
        List<ToDoItem> FindInTitle(string s);
        List<ToDoItem> GetPagedItemList(int size, int page);
    }
}