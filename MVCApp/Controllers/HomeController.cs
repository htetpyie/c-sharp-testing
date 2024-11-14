using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;
using Service.Class;
using Shared.Services;
using System.Diagnostics;

namespace MVCApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly QRService _qrService;
		private readonly ClassService _classService;

		public HomeController(ILogger<HomeController> logger, QRService qrService, ClassService classService)
		{
			_logger = logger;
			_qrService = qrService;
			this._classService = classService;
		}

		public async Task<IActionResult> IndexAsync()
		{
			//_qrService.GenerateURLQR("https://github.com/htetpyie");
			//await _classService.InsertStudent();
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
