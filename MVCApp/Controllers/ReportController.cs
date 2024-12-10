using AspNetCore.Reporting;
using FastReport.Export.PdfSimple;
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
			//var reportPath = Path.Combine(Environment.CurrentDirectory, "Reports", "EmployeeList.frx");
			//var report = new FastReport.Report();
			//report.Load(reportPath);
			//report.Prepare();
			//string outfolder = "Desktop";
			//if (!Directory.Exists(outfolder)) Directory.CreateDirectory(outfolder);
			//report.SavePrepared(Path.Combine(outfolder, "Prepared Report.fpx"));

			//// export to image
			//ImageExport image = new ImageExport();
			//image.ImageFormat = ImageExportFormat.Jpeg;
			//report.Export(image, Path.Combine(outfolder, "report.jpg"));
			//report.Dispose();

			var webReport = GetFastReport();

			return View(webReport);
		}


		[HttpGet]
		public IActionResult ExportReport(string format)
		{
			var webReport = GetFastReport();
			// Create a memory stream for the exported report
			MemoryStream stream = new MemoryStream();
			// Export based on the requested format
			switch (format.ToLower())
			{
				case "pdf":
					return ExportPdf(webReport);
				//case "excel":
				//	webReport.Report.Export(new FastReport.Export.OoXML.Excel2007Export(), stream);
				//	break;
				//case "word":
				//	webReport.Report.Export(new FastReport.Export.OoXML.Word2007Export(), stream);
				//	break;
				default:
					return BadRequest("Unsupported export format");
			}

		}

		public IActionResult ExportPdf(WebReport webReport)
		{
			webReport.Report.Prepare();
			using (MemoryStream ms = new MemoryStream())
			{
				PDFSimpleExport pdfExport = new PDFSimpleExport();
				pdfExport.Export(webReport.Report, ms);
				ms.Flush();
				return File(ms.ToArray(), "application/pdf", Path.GetFileNameWithoutExtension("Master-Detail") + ".pdf");
			}
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




		private WebReport GetFastReport()
		{
			var reportPath = Path.Combine(Environment.CurrentDirectory, "Reports", "EmployeeList.frx");
			var webReport = new WebReport();
			webReport.Report.Load(reportPath);
			//webReport.Toolbar.Exports.ShowPreparedReport = false;
			return webReport;
		}
		#endregion
	}
}
