using Microsoft.AspNetCore.Mvc;
using ToDo.Models;
using ToDo.Services;

namespace ToDo.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IToDoListSearchableService _todoListService;
        private readonly DapperRepository _dapperRepository;

        [Route("api/[controller]")]
        [HttpGet]
        public IActionResult GetTodos([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            //var res = _todoListService.GetPagedItemList(pageSize, page);
            var res = _dapperRepository.GetPagedToDoItems(pageSize, page);
            return Ok(res);
        }

        [Route("api/[controller]/search")]
        [HttpGet]
        public IActionResult SearchTodos([FromQuery] string q)
        {
            var res = _todoListService.FindInTitle(q);
            return Ok(res);
        }

        public ToDoController(IToDoListSearchableService todoListService, DapperRepository dapperRepository)
        {
            _todoListService = todoListService;
            _dapperRepository = dapperRepository;
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
            var todos = _dapperRepository.GetUnfinishedTodos();
            return Ok(todos);
        }
    }
}
