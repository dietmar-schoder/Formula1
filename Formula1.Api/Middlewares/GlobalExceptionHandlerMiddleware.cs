using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.ExternalServices;
using Formula1.Contracts.Responses;
using Formula1.Domain.Exceptions;
using System.Diagnostics;
using System.Text.Json;

namespace Formula1.Api.Middlewares;

public class GlobalExceptionHandlerMiddleware(
    RequestDelegate next,
    IHostEnvironment env)
{
    private readonly RequestDelegate _next = next;
    private readonly IHostEnvironment _env = env;

    public async Task InvokeAsync(
        HttpContext context,
        IScopedLogService logService,
        ISlackClient slackClient)
    {
        try
        {
            await _next(context);
        }
        catch (UserError ex)
        {
            await WriteErrorResponseAsync(context, ex.StatusCode, new ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            logService.AddText(ex.Source);
            logService.AddText(ex.StackTrace);
            var errorTextBlock = logService.GetLogsAsString(ex.Message);
            ErrorResponse responseBody = null;
            if (_env.IsDevelopment())
            {
                WriteToConsole(errorTextBlock);
                responseBody = new ErrorResponse(ex.Message, logService.GetLogsAsList());
            }
            else
            {
                slackClient.SendMessage($":boom: EXCEPTION: {errorTextBlock}");
            }
            await WriteErrorResponseAsync(context, StatusCodes.Status500InternalServerError, responseBody);
        }

        static async Task WriteErrorResponseAsync(HttpContext context, int statusCode, ErrorResponse responseBody)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(responseBody is null ? string.Empty : JsonSerializer.Serialize(responseBody));
        }

        static void WriteToConsole(string message)
        {
            Debug.WriteLine(string.Empty);
            Debug.WriteLine("== ERROR ==");
            Debug.WriteLine(message);
            Debug.WriteLine("===========");
            Debug.WriteLine(string.Empty);
        }
    }
}
