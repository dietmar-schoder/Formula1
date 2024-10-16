using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class SessionsEndpoints
{
    public static void MapSessionsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/sessions", ListSessionsAsync);
        app.MapGet("/api/sessions/{id:guid}", GetSessionAsync);

        static async Task<IResult> ListSessionsAsync(IMediator mediator)
            => Results.Ok(await mediator.Send(new GetSessionsQuery()));

        static async Task<IResult> GetSessionAsync(Guid id, IMediator mediator, IScopedErrorService errorService)
            => await mediator.SendQueryAsync(new GetSessionByIdQuery(id), errorService);
    }
}
