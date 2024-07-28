using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class ToDoItem
    {
        public  Guid Id { get; set; }

        [MaxLength(256)]
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}
