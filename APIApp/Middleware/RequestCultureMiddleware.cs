using APIApp.LiteDb;
using System.Globalization;

namespace APIApp.Middleware
{
	public class RequestCultureMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly LiteDbService _db;
		
		public RequestCultureMiddleware(RequestDelegate next, LiteDbService db)
		{
			_next = next;
			_db = db;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var cultureQuery = context.Request.Query["culture"];
			if (!string.IsNullOrWhiteSpace(cultureQuery))
			{
				var culture = new CultureInfo(cultureQuery);

				CultureInfo.CurrentCulture = culture;
				CultureInfo.CurrentUICulture = culture;
			}
			//_db.CreateCustomer();
			//var result = _db.GetAllCustomer();

			// Call the next delegate/middleware in the pipeline.
			await _next(context);
		}
	}

	public static class RequestCultureMiddlewareExtenstions
	{
		public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<RequestCultureMiddleware>();
		}

	}
}
