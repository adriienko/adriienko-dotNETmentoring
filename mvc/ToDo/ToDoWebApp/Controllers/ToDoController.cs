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
            var list = _todoListService.GetItemList();
            return View(list); 
        }

        [HttpPost]
        public IActionResult Create(ToDoItem model)
        {
            var id = _todoListService.Create(model);
            return RedirectToAction("ToDoIndex");
        }

        public IActionResult Edit(Guid id)
        {
            ToDoItem item; 
            if (id == Guid.Empty)
            {
                item = new ToDoItem();
            }
            else
            {
                item = _todoListService.Read(id);
            }
            return View(item);
        }

        [HttpPost]
        public IActionResult Update(ToDoItem model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit",model);
            }
            if (model.Id == Guid.Empty)
            {
                _todoListService.Create(model);
            }
            else
            {
                _todoListService.Update(model);
            }
            return RedirectToAction("ToDoIndex");
        }

        public IActionResult Delete(Guid id)
        {
            var item = _todoListService.Read(id);
            return View(item);
        }

        [HttpPost]
        public IActionResult DeleteBackEnd(Guid id)
        {
            _todoListService.Delete(id);
            return RedirectToAction("ToDoIndex");
        }

        [HttpGet]
        public ActionResult<IEnumerable<ToDoItem>> GetUnfinishedTodos()
        {
            var todos = _todoListService.GetUnfinishedTodos();
            return Ok(todos);
        }
    }
}
