using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Pricat.Api.Middleware;

namespace Pricat.Api.Filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values
                .Where(v => v.Errors.Count > 0)
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            var errorDetails = new ErrorDetails
            {
                ErrorType = "Bad Request",
                Errors = errors
            };

            context.Result = new BadRequestObjectResult(errorDetails);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}