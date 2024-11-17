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
        //app.MapGet("/api/circuits/{id:int}", GetCircuitAsync);

        //app.MapGet("/api/circuits/{id:int}/races", GetCircuitAsync);
        // app.MapGet("/api/circuits/{id:int}/sessions", GetCircuitAsync);
        //app.MapGet("/api/circuits/{id:int}/seasons", GetCircuitAsync);
        //app.MapGet("/api/circuits/{id:int}/results", GetCircuitAsync);
        //app.MapGet("/api/circuits/{id:int}/drivers", GetCircuitAsync);
        //app.MapGet("/api/circuits/{id:int}/constructors", GetCircuitAsync);

        static async Task<IResult> ListCircuitsAsync(IMediator mediator, int pageNumber = 1, int PageSize = 20)
            => Results.Ok(await mediator.Send(new GetCircuits.Query(pageNumber, PageSize)));

        //static async Task<IResult> GetCircuitAsync(int id, IMediator mediator, IScopedErrorService errorService)
        //    => await mediator.SendQueryAsync(new GetCircuitByIdQuery(id), errorService);
    }
}
