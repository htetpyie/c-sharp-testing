using APIApp.Controllers.SerilogTest;
using APIApp.LiteDb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace APIApp.Controllers.MiddlewareTesting
{
    [Route("api/[controller]")]
	[ApiController]
	public class MiddlewareTestController : BaseController
	{
		private readonly LiteDbService _liteDb;

		public MiddlewareTestController(LiteDbService liteDb)
		{
			_liteDb = liteDb;
		}

		[EnableRateLimiting("fixed")]
		[HttpGet("get-data")]
		public IActionResult Get()
		{
			var customers = _liteDb.GetAllCustomer();
			return Ok(customers);
		}

		[HttpGet("create")]
		public IActionResult CreateCustomer()
		{
			_liteDb.CreateCustomer();
			return Ok("Customer is created.");
		}
	}
}
