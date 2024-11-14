using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;

namespace MVCApp.Controllers
{
	public class ReportController : Controller
	{
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
	}
}
