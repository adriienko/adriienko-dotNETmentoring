using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class ToDoItem
    {
        public  Guid Id { get; set; }
        
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
    }
}
