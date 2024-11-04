
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace TodoItems.Api.Filter;
public class CustomExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger<CustomExceptionFilter> _logger;

    public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
    {
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "An unhandled exception occurred.");

        context.Result = new JsonResult(new
        {
            Error = "An error occurred while processing your request.",
            Details = context.Exception.Message
        })
        {
            StatusCode = 500
        };

        context.ExceptionHandled = true;
    }
}
