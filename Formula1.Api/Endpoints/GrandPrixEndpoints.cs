using Formula1.Application.Handlers.QueryHandlers;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class GrandPrixEndpoints
{
    public static void MapGrandPrixEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/grandprix", ListGrandPrixAsync);
        app.MapGet("/api/grandprix/{id:guid}", GetGrandPrixAsync);

        app.MapGet("/api/grandprix/{id:guid}/races", GetGrandPrixAsync);
        app.MapGet("/api/grandprix/{id:guid}/seasons", GetGrandPrixAsync);
        app.MapGet("/api/grandprix/{id:guid}/circuits", GetGrandPrixAsync);
        // app.MapGet("/api/grandprix/{id:guid}/sessions", GetGrandPrixAsync);
        app.MapGet("/api/grandprix/{id:guid}/results", GetGrandPrixAsync);
        app.MapGet("/api/grandprix/{id:guid}/constructors", GetGrandPrixAsync);
        app.MapGet("/api/grandprix/{id:guid}/drivers", GetGrandPrixAsync);

        static async Task<IResult> ListGrandPrixAsync(IMediator mediator,
            int pageNumber = 1,
            int PageSize = 20)
            => Results.Ok(await mediator.Send(new GetGrandPrix.Query(pageNumber, PageSize)));

        static async Task<IResult> GetGrandPrixAsync(Guid id, IMediator mediator, IScopedErrorService errorService)
            => await mediator.SendQueryAsync(new GetGrandPrixByIdQuery(id), errorService);
    }
}
