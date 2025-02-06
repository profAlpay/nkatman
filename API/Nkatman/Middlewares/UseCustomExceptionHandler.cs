﻿using Microsoft.AspNetCore.Diagnostics;
using nkatman.Core.DTOs;
using nkatman.Service.Exceptions;
using System.Text.Json;

namespace Nkatman.API.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        NotFoundException => 404,
                        _=> 500
                    };
                    context.Response.StatusCode = statusCode;

                    var response = new CustomResponseDto<NoContentDto>().Fail(statusCode, exceptionFeature.Error.Message);
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }

    }
}
