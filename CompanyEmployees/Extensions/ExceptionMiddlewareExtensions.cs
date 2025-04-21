using System;
using System.Net;
using Azure.Core;
using Contract.Interfaces;
using Entities.ErrorModel;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.SqlServer.Server;

namespace CompanyEmployees.Extensions;

public static class ExceptionMiddlewareExtensions
{
    /// <summary>
    /// Once you send the request, Visual Studio will stop the execution inside
    /// the GetCompanies action on the line where we throw an exception.This is
    /// normal behavior and all you have to do is to click the continue button to finish
    /// the request flow. Additionally, you can start your app with CTRL+F5, which will
    /// prevent Visual Studio from stopping the execution.Also, if you want to start
    /// your app with F5 but still to avoid VS execution stoppages, you can open the
    /// Tools->Options->Debugging->General option and uncheck the Enable Just My
    /// Code checkbox.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="logger"></param>
    public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if(contextFeature != null)
                {
                    logger.LogError($"Somthing went wrong {contextFeature.Error}");

                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "Internal Server Error."
                    }.ToString());
                }
            });
        });
    }
}
