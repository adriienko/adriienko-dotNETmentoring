using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("")]
    public class EchoController : ControllerBase
    {
        [HttpGet("{text}")]
        public string Get(string text)
        {
            return "Answer: " + text;
        }
    }
}
