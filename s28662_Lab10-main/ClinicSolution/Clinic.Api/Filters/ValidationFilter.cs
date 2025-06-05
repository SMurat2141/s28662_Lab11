using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Clinic.Api.Filters;

public class ValidationFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ValidationException vex)
        {
            var details = new ValidationProblemDetails(new Dictionary<string, string[]>()
            {
                ["validation"] = new[] { vex.Message }
            })
            {
                Title = "Validation failed",
                Status = StatusCodes.Status400BadRequest
            };

            context.Result = new BadRequestObjectResult(details);
            context.ExceptionHandled = true;
        }
    }
}