using Microsoft.AspNetCore.Mvc;

namespace ToDo.Controllers
{
    public class MapController : Controller
    {
        public IActionResult MapIndex() 
        {
            var list = new List<object>() { "1","2",3,4,5,};

            ViewData["name"] = "Vasya";
            ViewBag.Sum = 10000;
            return View(list); 
        }
    }
}
