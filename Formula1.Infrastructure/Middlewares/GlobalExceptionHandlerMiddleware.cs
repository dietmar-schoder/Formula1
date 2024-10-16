using Formula1.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Formula1.Infrastructure.Middlewares;

public class GlobalExceptionHandlerMiddleware(
    RequestDelegate next,
    IHostEnvironment hostEnvironment)
{
    private readonly RequestDelegate _next = next;
    private readonly IHostEnvironment _hostEnvironment = hostEnvironment;

    public async Task InvokeAsync(HttpContext context, IExceptionService exceptionService)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await (_hostEnvironment.IsDevelopment()
                ? exceptionService.HandleExceptionInDevelopmentAsync(exception)
                : exceptionService.HandleExceptionInProductionAsync(exception));
        }
    }
}
