using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;
using Models.Blog;
using Shared.DbServices;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;

namespace MVCApp.Controllers
{
	public class ReportController : Controller
	{

		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly JsonService _jsonService;

		public ReportController(IWebHostEnvironment webHostEnvironment, JsonService jsonService)
		{
			_webHostEnvironment = webHostEnvironment;
			_jsonService = jsonService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult GetReport()
		{
			try
			{
				var report = StiReport.CreateNewReport();
				var path = StiNetCoreHelper.MapPath(this, "Reports/Report.mrt");
				report.Load(path);

				return StiNetCoreViewer.GetReportResult(this, report);
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public IActionResult ViewerEvent()
		{
			return StiNetCoreViewer.ViewerEventResult(this);
		}

		#region RDLC
		public IActionResult RDLC()
		{
			try
			{
				string mimtype = "";
				int extnesion = 1;

				var jsonFilePath = "E:\\HPPM\\Projects\\c-sharp-testing\\Shared\\Jsons\\blog_data.json";
				var reportPath = $"{_webHostEnvironment.WebRootPath}\\Reports\\Blog.rdlc";

				var parameters = new Dictionary<string, string>();

				var blogList = _jsonService.ReadJson<BlogDataModel>(jsonFilePath);

				var blog = blogList.FirstOrDefault(x => x.Id == 1) ?? new();

				parameters.Add("title", blog.Title);
				parameters.Add("author", blog.Author);
				var localReport = new LocalReport(reportPath);

				var result = localReport.Execute(RenderType.Pdf, extnesion, parameters);
				return File(result.MainStream, "application/pdf");
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		#endregion
	}
}
