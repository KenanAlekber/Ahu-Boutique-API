using Ahu.Business.DTOs.CommonDtos;
using Ahu.Business.Exceptions;
using Ahu.Core.Entities.Common;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Ahu.API.Extension;

public static class ExceptionHandlerServiceExtension
{
    public static IApplicationBuilder AddExcepitonHandler(this IApplicationBuilder application)
    {
        application.UseExceptionHandler(error =>
        {
            error.Run(async context =>
            {
                var feature = context.Features.Get<IExceptionHandlerFeature>();

                HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
                string message = "Unexpected error occured";
                Guid id = Guid.NewGuid();

                if (feature.Error is IBaseException)
                {
                    var exception = (IBaseException)feature.Error;
                    id = exception.Id;
                    statusCode = exception.StatusCode;
                    message = exception.ErrorMessage;
                }

                var response = new ResponseDto(id, statusCode, message);

                context.Response.StatusCode = (int)statusCode;
                await context.Response.WriteAsJsonAsync(response);
                await context.Response.CompleteAsync();
            });
        });

        return application;
    }
}