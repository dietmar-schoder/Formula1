using Formula1.Application.Handlers.QueryHandlers;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class RacesEndpoints
{
    public static void MapRacesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/races", ListRacesAsync);
        app.MapGet("/api/races/{id:guid}", GetRaceAsync);

        app.MapGet("/api/races/{id:guid}/sessions", GetRaceAsync);
        app.MapGet("/api/races/{id:guid}/results", GetRaceAsync);
        app.MapGet("/api/races/{id:guid}/constructors", GetRaceAsync);
        app.MapGet("/api/races/{id:guid}/drivers", GetRaceAsync);

        static async Task<IResult> ListRacesAsync(IMediator mediator,
            int pageNumber = 1,
            int PageSize = 20)
            => Results.Ok(await mediator.Send(new GetRaces.Query(pageNumber, PageSize)));

        static async Task<IResult> GetRaceAsync(Guid id, IMediator mediator, IScopedErrorService errorService)
            => await mediator.SendQueryAsync(new GetRaceByIdQuery(id), errorService);
    }
}
