using System.Net;
using Core.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LuftbornDemo.WebApi.Middleware;
public static class ExceptionMiddleware
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    var error = contextFeature.Error;
                    var statusCode = error switch
                    {
                        NotFoundException => HttpStatusCode.NotFound,
                       // BadRequestException => HttpStatusCode.BadRequest,
                        _ => HttpStatusCode.InternalServerError
                    };

                    context.Response.StatusCode = (int)statusCode;

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                    {
                        StatusCode = statusCode,
                        Message = error.Message
                    }));
                }
            });
        });
    }
}