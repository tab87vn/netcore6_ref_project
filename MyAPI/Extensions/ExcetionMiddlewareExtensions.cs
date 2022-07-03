using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using tab.TestDotNet.API.Exceptions;
using tab.TestDotNet.API.Models;
using tab.TestDotNet.Services.Contracts;

namespace tab.TestDotNet.API.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(
        this WebApplication app, ILoggerManager logger)
    {
        app.UseExceptionHandler(error => {
            error.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        NotFoundException => (int) HttpStatusCode.NotFound,
                        _ => (int) HttpStatusCode.InternalServerError
                    };

                    // logs actual error
                    logger.LogError($"Something went wrong: {contextFeature.Error}");
                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        // Message = "Internal Server Error.", // returns general message
                        Message = contextFeature.Error.Message, // returns general message
                    }.ToString());
                }
            });
        });
    }
}