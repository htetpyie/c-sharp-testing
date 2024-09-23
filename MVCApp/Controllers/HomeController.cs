using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;
using Shared.Services;
using System.Diagnostics;

namespace MVCApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly QRService _qrService;

		public HomeController(ILogger<HomeController> logger, QRService qrService)
		{
			_logger = logger;
			_qrService = qrService;
		}

		public IActionResult Index()
		{
			_qrService.GenerateURLQR("https://github.com/htetpyie");
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
