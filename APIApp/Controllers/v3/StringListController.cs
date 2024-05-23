using APIApp.Controllers.v1;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace APIApp.Controllers.v3;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("3.0")]
public class StringListController : ControllerBase
{
    [HttpGet()]
    public IEnumerable<string> GetString()
    {
        return Data.Summaries.Where(x => x.StartsWith("S"));
    }
}
