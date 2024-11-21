using Formula1.Application.Handlers.QueryHandlers;
using Formula1.Application.Interfaces.Services;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class SessionsEndpoints
{
    public static void MapSessionsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/sessions", ListSessionsAsync);
        app.MapGet("/api/sessions/{id:int}", GetSessionAsync);
        app.MapGet("/api/sessions/{id:int}/results", GetSessionResultsAsync);
        //app.MapGet("/api/sessions/{id:int}/constructors", GetSessionAsync);
        //app.MapGet("/api/sessions/{id:int}/drivers", GetSessionAsync);

        static async Task<IResult> ListSessionsAsync(IMediator mediator, int pageNumber = 1, int PageSize = 20)
            => Results.Ok(await mediator.Send(new GetSessions.Query(pageNumber, PageSize)));

        static async Task<IResult> GetSessionAsync(int id, IMediator mediator)
            => await mediator.SendQueryAsync(new GetSession.Query(id));

        static async Task<IResult> GetSessionResultsAsync(IMediator mediator, int id, int pageNumber = 1, int pageSize = 20)
            => await mediator.SendQueryAsync(new GetSessionResults.Query(id, pageNumber, pageSize));
    }
}
