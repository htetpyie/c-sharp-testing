using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AspNet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly Microsoft.Extensions.Logging.ILogger _logger;


        //this method use logger factory with configuration in program.cs
        public BaseController(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger("base");
        }

        public BaseController()
        {
            if(_logger is null)
            {
                var logger = new LoggerFactory().AddSerilog();
                _logger = logger.CreateLogger("base");
            }
        }

        protected void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        protected void LogError(Exception ex)
        {
            var controllerName = ControllerContext.ActionDescriptor.ControllerName;
            var actionName = ControllerContext.ActionDescriptor.ActionName;

            _logger.LogError(ex.Message);
        }
    }
}
