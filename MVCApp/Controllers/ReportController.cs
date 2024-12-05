using AspNetCore.Reporting;
using FastReport.Export.Image;
using FastReport.Web;
using Microsoft.AspNetCore.Mvc;
using Models.Blog;
using Shared.DbServices;
using Shared.Services;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using System.Data;

namespace MVCApp.Controllers
{
	public class ReportController : Controller
	{

		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly JsonService _jsonService;
		private readonly DataSetService _dataSetService;

		public ReportController(IWebHostEnvironment webHostEnvironment, JsonService jsonService, DataSetService dataSetService)
		{
			_webHostEnvironment = webHostEnvironment;
			_jsonService = jsonService;
			_dataSetService = dataSetService;
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

		#region Fast Report
		public IActionResult FastReport(int? reportIndex = 0)
		{
			var report = new FastReport.Report();
			report.Load(Path.Combine(Environment.CurrentDirectory, "Reports", "EmployeeList.frx"));

			report.Prepare();

			string outfolder = "Desktop";
			if (!Directory.Exists(outfolder)) Directory.CreateDirectory(outfolder);
			report.SavePrepared(Path.Combine(outfolder, "Prepared Report.fpx"));

			// export to image
			ImageExport image = new ImageExport();
			image.ImageFormat = ImageExportFormat.Jpeg;
			report.Export(image, Path.Combine(outfolder, "report.jpg"));

			report.Dispose();

			return View();
		}

		public IActionResult Generate()
		{
			WebReport webReport = new WebReport();
			webReport.Report.Load($"{Directory.GetCurrentDirectory()}/Reports/report.frx");

			// Load data into the report
			DataSet dataSet = new DataSet();
			dataSet.ReadXml($"{Directory.GetCurrentDirectory()}/Data/data.xml");
			webReport.Report.RegisterData(dataSet, "Data");

			return View(webReport);
		}

		#endregion
	}
}
