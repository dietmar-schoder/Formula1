using Formula1.Application.Handlers.QueryHandlers;
using Formula1.Application.Interfaces.Services;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class CircuitsEndpoints
{
    public static void MapCircuitsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/circuits", ListCircuitsAsync);
        app.MapGet("/api/circuits/{id:int}", GetCircuitAsync);
        app.MapGet("/api/circuits/{id:int}/races", GetCircuitRacesAsync);
        //app.MapGet("/api/circuits/{id:int}/sessions", GetCircuitAsync);
        //app.MapGet("/api/circuits/{id:int}/seasons", GetCircuitAsync);
        //app.MapGet("/api/circuits/{id:int}/results", GetCircuitAsync);
        //app.MapGet("/api/circuits/{id:int}/drivers", GetCircuitAsync);
        //app.MapGet("/api/circuits/{id:int}/constructors", GetCircuitAsync);

        static async Task<IResult> ListCircuitsAsync(IMediator mediator, int pageNumber = 1, int PageSize = 20)
            => Results.Ok(await mediator.Send(new GetCircuits.Query(pageNumber, PageSize)));

        static async Task<IResult> GetCircuitAsync(IMediator mediator, int id)
            => await mediator.SendQueryAsync(new GetCircuit.Query(id));

        static async Task<IResult> GetCircuitRacesAsync(IMediator mediator, int id, int pageNumber = 1, int PageSize = 20)
            => await mediator.SendQueryAsync(new GetCircuitRaces.Query(id, pageNumber, PageSize));
    }
}
