using Formula1.Application.Handlers.QueryHandlers;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class CircuitsEndpoints
{
    public static void MapCircuitsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/circuits", ListCircuitsAsync);
        app.MapGet("/api/circuits/{id:guid}", GetCircuitAsync);

        app.MapGet("/api/circuits/{id:guid}/races", GetCircuitAsync);
        // app.MapGet("/api/circuits/{id:guid}/sessions", GetCircuitAsync);
        app.MapGet("/api/circuits/{id:guid}/seasons", GetCircuitAsync);
        app.MapGet("/api/circuits/{id:guid}/results", GetCircuitAsync);
        app.MapGet("/api/circuits/{id:guid}/drivers", GetCircuitAsync);
        app.MapGet("/api/circuits/{id:guid}/constructors", GetCircuitAsync);

        static async Task<IResult> ListCircuitsAsync(IMediator mediator,
            int pageNumber = 1,
            int PageSize = 20)
            => Results.Ok(await mediator.Send(new GetCircuits.Query(pageNumber, PageSize)));

        static async Task<IResult> GetCircuitAsync(Guid id, IMediator mediator, IScopedErrorService errorService)
            => await mediator.SendQueryAsync(new GetCircuitByIdQuery(id), errorService);
    }
}
