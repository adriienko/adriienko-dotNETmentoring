using Microsoft.AspNetCore.Mvc;
using ToDo.Models;
using ToDo.Services;

namespace ToDo.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IToDoListService _todoListService;

        public ToDoController(IToDoListService todoListService)
        {
            _todoListService = todoListService;
        }

        public IActionResult ToDoIndex() 
        {
            //var list = new List<object>() { "1","2",3,4,5,};
            //ViewData["name"] = "Vasya";
            //ViewBag.Sum = 10000;

            var list = _todoListService.GetItemList();
            return View(list); 
        }
    }
}
