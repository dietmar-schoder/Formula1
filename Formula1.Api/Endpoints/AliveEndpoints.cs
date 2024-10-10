using Formula1.Application.Queries;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class AliveEndpoints
{
    public static void MapAliveEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", GetVersionAsync).WithOpenApi();
        app.MapGet("/api", GetVersionAsync).WithOpenApi();

        static async Task<IResult> GetVersionAsync(IMediator mediator)
            => Results.Ok(await mediator.Send(new GetVersionQuery()));
    }
}
