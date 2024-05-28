using Microsoft.AspNetCore.Mvc.Filters;

namespace APIApp.Controllers.FilterTesting.ActionFilter;

public class ActionFilterSample : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Do something before the action executes.
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Do something after the action executes.
    }
}
