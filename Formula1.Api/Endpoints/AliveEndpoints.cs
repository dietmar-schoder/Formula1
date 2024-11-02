using Formula1.Application.Handlers.QueryHandlers;
using Formula1.Application.Interfaces.Services;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class AliveEndpoints
{
    public static void MapAliveEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", GetVersionAsync);
        app.MapGet("/api", GetVersionAsync);
        app.MapGet("/error", ThrowError);

        static async Task<IResult> GetVersionAsync(IMediator mediator)
            => Results.Ok(await mediator.Send(new GetVersion.Query()));

        static IResult ThrowError(IScopedLogService logService)
        {
            logService.Log("Before error");
            var zero = 0;
            var y = 1 / zero;
            logService.Log("After error");
            return Results.Ok();
        }
    }
}
