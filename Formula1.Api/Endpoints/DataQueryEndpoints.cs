using Formula1.Application.Queries;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class DataQueryEndpoints
{
    public static void MapDataQueryEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/sessions", ListSessionsAsync);
        app.MapGet("/api/sessions/{id:guid}", GetSessionAsync);
        app.MapGet("/api/sessiontypes", ListSessionTypesAsync);

        static async Task<IResult> ListSessionsAsync(IMediator mediator)
            => Results.Ok(await mediator.Send(new GetSessionsQuery()));

        static async Task<IResult> GetSessionAsync(Guid id, IMediator mediator)
            => Results.Ok(await mediator.Send(new GetSessionByIdQuery(id)));

        static async Task<IResult> ListSessionTypesAsync(IMediator mediator)
            => Results.Ok(await mediator.Send(new GetSessionTypesQuery()));
    }
}
