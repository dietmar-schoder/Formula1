using Formula1.Application.Interfaces.Services;

namespace Formula1.Api.Middlewares;

public class GlobalExceptionHandlerMiddleware(
    RequestDelegate next,
    IHostEnvironment hostEnvironment)
{
    private readonly RequestDelegate _next = next;
    private readonly IHostEnvironment _hostEnvironment = hostEnvironment;
    //private readonly  = scopedErrorService;

    public async Task InvokeAsync(HttpContext context, IScopedErrorService scopedErrorService)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await (_hostEnvironment.IsDevelopment()
                ? scopedErrorService.HandleExceptionInDevelopmentAsync(exception)
                : scopedErrorService.HandleExceptionInProductionAsync(exception));
        }
    }
}
