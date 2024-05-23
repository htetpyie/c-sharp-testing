using APIApp.Controllers.v1;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace APIApp.Controllers.v2;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("2.0")]
public class StringListController : ControllerBase
{
    [HttpGet()]
    public IEnumerable<string> GetString()
    {
        return Data.Summaries.Where(x => x.StartsWith("C"));
    }
}
