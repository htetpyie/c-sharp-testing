using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace APIApp.Controllers.v1;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1.0", Deprecated = true)]
public class StringListController : ControllerBase
{
    //API version 1 which retrieve only strings that start with Letter B
    [HttpGet()]
    public IEnumerable<string> GetString()
    {
        return Data.Summaries.Where(x => x.StartsWith("B"));
    }
}

public class Data
{
    public static readonly string[] Summaries = new[]
    {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};
}
