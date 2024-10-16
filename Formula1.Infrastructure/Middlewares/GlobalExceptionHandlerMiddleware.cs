using Formula1.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Formula1.Infrastructure.Middlewares;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, IExceptionService exceptionService)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await exceptionService.HandleExceptionAsync(exception);
        }
    }
}
