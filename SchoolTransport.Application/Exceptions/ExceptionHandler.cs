using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using SchoolTransport.Application.Exceptions.Base;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SchoolTransport.Application.Exceptions
{
    public static class ExceptionHandler
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app , ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                       
                        var exception = contextFeature.Error;

                        var (statusCode, message) = exception switch
                        {
                            BaseBadRequestException badRequestEx => ((int)HttpStatusCode.BadRequest, badRequestEx.Message),
                            BaseNotFoundException notFoundEx => ((int)HttpStatusCode.NotFound, notFoundEx.Message),
                            _ => ((int)HttpStatusCode.InternalServerError, "Internal Server Error.")
                        };

                        context.Response.StatusCode = statusCode;
                        context.Response.ContentType = "application/json";

                        // Loglamayı ILogger üzerinden yapıyoruz.
                        logger.LogError(exception, $"Something went wrong: {exception.Message}");

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = statusCode,
                            Message = message
                        }.ToString());
                    }
                });
            });
        }
    }
}