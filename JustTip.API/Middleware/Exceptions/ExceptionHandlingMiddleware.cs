using JustTip.Application.Logging;
using Microsoft.Extensions.Options;

namespace JustTip.API.Middleware.Exceptions;

//########################################//


public record ExceptionHandlingMiddlewareOptions(IExceptionConverter Converter);


//########################################//


public class ExceptionHandlingMiddleware(
    RequestDelegate _next,
    ILogger<ExceptionHandlingMiddleware> _logger,
    IOptions<ExceptionHandlingMiddlewareOptions> optsProvider)
{
    private readonly IExceptionConverter _converter = optsProvider.Value.Converter;

    //--------------------------//

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            //var exceptionType = exception.GetType();
            //var innerExceptionType = exception.InnerException?.GetType();
            //var innerExFullname = innerExceptionType?.FullName;

            // CA2254 fix: Use a constant message template and pass exception.Message as an argument
            _logger.LogError(
                new EventId(JtEventIds.Errors.Unexpected),
                exception,
                "An unexpected error occurred: {ErrorMessage}",
                exception.Message);

            var exceptionDetails = _converter.GetExceptionDetails(exception);
            context.Response.StatusCode = exceptionDetails.Status;
            await context.Response.WriteAsJsonAsync(exceptionDetails.Details);
        }

    }


}//Cls


//########################################//


public static class ExceptionHandlingMiddlewareExtensions
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app, IExceptionConverter? converter = null)
    {
        var options = new ExceptionHandlingMiddlewareOptions(converter ?? new JtExceptionConverter());
        app.UseMiddleware<ExceptionHandlingMiddleware>(Options.Create(options));
    }

}//Cls


//########################################//