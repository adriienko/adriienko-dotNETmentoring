
using ToDo.Models;

namespace ToDo.Services
{
    public interface IToDoListService
    {
        Guid Add(ToDoItem item);
        void Edit(ToDoItem item);
        void Remove(ToDoItem item);
        List<ToDoItem> GetItemList();
    }
}