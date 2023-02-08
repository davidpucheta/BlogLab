using System.Net;
using BlogLab.Models.Exception;
using Microsoft.AspNetCore.Diagnostics;

namespace BlogLab.Web.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async ctx =>
            {
                ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ctx.Response.ContentType = "application/json";

                var ctxFeature = ctx.Features.Get<IExceptionHandlerFeature>();
                if (ctxFeature != null)
                {
                    //In prod you would log ex to DB 
                    await ctx.Response.WriteAsync(new ApiException
                    {
                        StatusCode = ctx.Response.StatusCode,
                        Message = "Internal Server Error"
                    }.ToString());
                }
            });
        });
    }
}