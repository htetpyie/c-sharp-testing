using Microsoft.AspNetCore.Mvc;

namespace APIApp.Controllers.FilterTesting.ActionFilter
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActionFilterTestController : ControllerBase
    {
        [HttpGet("Test")]
        public IActionResult TestActionFilter()
        {
            return Ok("Nice");
        }
    }
}
