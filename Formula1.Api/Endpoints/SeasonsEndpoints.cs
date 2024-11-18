using Formula1.Application.Handlers.QueryHandlers;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class SeasonsEndpoints
{
    public static void MapSeasonsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/seasons", ListSeasonsAsync);
        app.MapGet("/api/seasons/{year:int}", GetSeasonAsync);
        //app.MapGet("/api/seasons/{year:int}/drivers", GetSeasonDriversAsync);
        app.MapGet("/api/seasons/{year:int}/drivers/{id:int}/results", GetSeasonDriverResultsAsync);
        //app.MapGet("/api/seasons/{year:int}/constructors", GetSeasonConstructorsAsync);
        app.MapGet("/api/seasons/{year:int}/constructors/{id:int}/results", GetSeasonConstructorResultsAsync);

        //app.MapGet("/api/seasons/{year:int}/races", GetSeasonAsync);
        //app.MapGet("/api/seasons/{year:int}/circuits", GetSeasonAsync);
        //app.MapGet("/api/seasons/{year:int}/sessions", GetSeasonAsync);
        //app.MapGet("/api/seasons/{year:int}/results", GetSeasonAsync);

        static async Task<IResult> ListSeasonsAsync(IMediator mediator, int pageNumber = 1, int PageSize = 20)
            => Results.Ok(await mediator.Send(new GetSeasons.Query(pageNumber, PageSize)));

        static async Task<IResult> GetSeasonAsync(IMediator mediator, int year)
            => await mediator.SendQueryAsync(new GetSeasonByYearQuery(year));

        //static async Task<IResult> GetSeasonDriversAsync(IMediator mediator, IScopedErrorService errorService, int year)
        //    => await mediator.SendQueryAsync(new GetSeasonDrivers.Query(year), errorService);

        static async Task<IResult> GetSeasonDriverResultsAsync(IMediator mediator, int year, int id)
            => await mediator.SendQueryAsync(new GetSeasonDriverResults.Query(year, id));

        //static async Task<IResult> GetSeasonConstructorsAsync(IMediator mediator, IScopedErrorService errorService, int year)
        //    => await mediator.SendQueryAsync(new GetSeasonConstructors.Query(year), errorService);

        static async Task<IResult> GetSeasonConstructorResultsAsync(IMediator mediator, int year, int id)
            => await mediator.SendQueryAsync(new GetSeasonConstructorResults.Query(year, id));
    }
}
