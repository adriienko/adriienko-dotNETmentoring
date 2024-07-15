using ToDo.Models;

namespace ToDo.Services
{

    public class ToDoListService : IToDoListService
    {
        private readonly string _notFoudMessage = "item not found";

        private readonly Dictionary<Guid, string> Storage = new Dictionary<Guid, string>();

        public ToDoListService()
        {
            Add(new ToDoItem { Id = Guid.NewGuid(), Title = "item1", });
            Add(new ToDoItem { Id = Guid.NewGuid(), Title = "item2", });
            Add(new ToDoItem { Id = Guid.NewGuid(), Title = "item3", });
        }

        public List<ToDoItem> GetItemList()
        {
            List<ToDoItem> todos = Storage.Select(pair =>
                                        new ToDoItem { Id = pair.Key, Title = pair.Value })
                                             .ToList();
            return todos;
        }

        public Guid Add(ToDoItem item)
        {
            var id = Guid.NewGuid();
            Storage.Add(id, item.Title);
            return id;
        }

        public void Remove(ToDoItem item)
        {
            if (Storage.ContainsKey(item.Id))
            {
                Storage.Remove(item.Id);
            }
            else
            {
                throw new Exception(_notFoudMessage);
            }
        }

        public void Edit(ToDoItem item)
        {
            if (Storage.ContainsKey(item.Id))
            {
                Storage.Remove(item.Id);
            }
            else
            {
                throw new Exception(_notFoudMessage);
            }
        }
    }
}
