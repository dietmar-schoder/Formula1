using Formula1.Application.Handlers.QueryHandlers;
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

        app.MapGet("/api/sessions/{id:guid}/results", GetSessionAsync);
        app.MapGet("/api/sessions/{id:guid}/constructors", GetSessionAsync);
        app.MapGet("/api/sessions/{id:guid}/drivers", GetSessionAsync);

        static async Task<IResult> ListSessionsAsync(IMediator mediator,
            int pageNumber = 1,
            int PageSize = 20)
            => Results.Ok(await mediator.Send(new GetSessions.Query(pageNumber, PageSize)));

        static async Task<IResult> GetSessionAsync(Guid id, IMediator mediator, IScopedErrorService errorService)
            => await mediator.SendQueryAsync(new GetSessionByIdQuery(id), errorService);
    }
}
