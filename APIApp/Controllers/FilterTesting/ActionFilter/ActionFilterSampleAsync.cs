using Microsoft.AspNetCore.Mvc.Filters;

namespace APIApp.Controllers.FilterTesting.ActionFilter;

public class ActionFilterSampleAsync : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Console.WriteLine("In Action Filter Async Method");
        // Do something before the action executes.
        await next();
        // Do something after the action executes.        }
    }
}