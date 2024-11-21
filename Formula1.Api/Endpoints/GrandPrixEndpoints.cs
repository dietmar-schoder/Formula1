using Formula1.Application.Handlers.QueryHandlers;
using Formula1.Application.Interfaces.Services;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class GrandPrixEndpoints
{
    public static void MapGrandPrixEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/grandprix", ListGrandPrixAsync);
        app.MapGet("/api/grandprix/{id:int}", GetGrandPrixAsync);

        //app.MapGet("/api/grandprix/{id:int}/races", GetGrandPrixAsync);
        //app.MapGet("/api/grandprix/{id:int}/seasons", GetGrandPrixAsync);
        //app.MapGet("/api/grandprix/{id:int}/circuits", GetGrandPrixAsync);
        //app.MapGet("/api/grandprix/{id:guid}/sessions", GetGrandPrixAsync);
        //app.MapGet("/api/grandprix/{id:int}/results", GetGrandPrixAsync);
        //app.MapGet("/api/grandprix/{id:int}/constructors", GetGrandPrixAsync);
        //app.MapGet("/api/grandprix/{id:int}/drivers", GetGrandPrixAsync);

        static async Task<IResult> ListGrandPrixAsync(IMediator mediator, int pageNumber = 1, int PageSize = 20)
            => Results.Ok(await mediator.Send(new GetGrandPrixList.Query(pageNumber, PageSize)));

        static async Task<IResult> GetGrandPrixAsync(IMediator mediator, int id)
            => await mediator.SendQueryAsync(new GetGrandPrix.Query(id));
    }
}
