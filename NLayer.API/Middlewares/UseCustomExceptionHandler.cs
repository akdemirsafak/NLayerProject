using Microsoft.AspNetCore.Diagnostics;
using NLayer.Core.DTOs;
using NLayer.Service.Exceptions;

namespace NLayer.API.Middlewares;

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
                //bu interfaceden fırlatılan exception u yakalıyoruz.
                var statusCode = exceptionFeature.Error switch
                {
                    ClientSideException => 400,
                    NotFoundException => 404,
                    _ => 500
                };
                //Uygulama hata fırlatabilir 500 veya biz hata fırlatabiliriz(client ın bir hatasından dolayı) 400 dönmemiz gerekir.
                //Burada ayrım yapmak için uygulama içerisinde fırlatacağımız hataları ayırmamız gerekir.
                context.Response.StatusCode = statusCode;
                var response = CustomResponseDto<NoContentDto>.Fail(statusCode, exceptionFeature.Error.Message);
                
                await context.Response.WriteAsJsonAsync(response); //await context.Response.WriteAsync(JsonSerializer.Serialize(response));

            });
        });
    }
}