using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace APIApp.Controllers.MiddlewareTesting
{
	[Route("api/[controller]")]
	[ApiController]
	public class MiddlewareTestController : BaseController
	{
		[EnableRateLimiting("fixed")]
		[HttpGet("get-data")]
		public IActionResult Get()
		{
			return Ok("Get data api is called!");
		}
	}
}
