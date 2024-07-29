using ToDo.Models;

namespace ToDo.Services
{

    public class ToDoListService : IToDoListService
    {
        private readonly string _notFoudMessage = "item not found";

        private readonly Dictionary<Guid, string> Storage = new Dictionary<Guid, string>();

        public ToDoListService()
        {
            Create(new ToDoItem { Id = Guid.NewGuid(), Title = "item1", });
            Create(new ToDoItem { Id = Guid.NewGuid(), Title = "item2", });
            Create(new ToDoItem { Id = Guid.NewGuid(), Title = "item3", });
        }

        public List<ToDoItem> GetItemList()
        {
            List<ToDoItem> todos = Storage.Select(pair =>
                                        new ToDoItem { Id = pair.Key, Title = pair.Value })
                                             .ToList();
            return todos;
        }

        public Guid Create(ToDoItem item)
        {
            var id = Guid.NewGuid();
            Storage.Add(id, item.Title);
            return id;
        }

        public ToDoItem Read(Guid id)
        {
            if (Storage.ContainsKey(id))
            {    
                return new ToDoItem() { Id = id, Title = Storage[id]};
            }
            else
            {
                throw new Exception(_notFoudMessage);
            }
        }

        public void Delete(Guid id)
        {
            Storage.Remove(id);
        }

        public void Update(ToDoItem item)
        {
            if (Storage.ContainsKey(item.Id))
            {
                Storage[item.Id] = item.Title;
            }
            else
            {
                throw new Exception(_notFoudMessage);
            }
        }

        public IEnumerable<ToDoItem> GetUnfinishedTodos()
        {
            throw new NotImplementedException();
        }
    }
}
