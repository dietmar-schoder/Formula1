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

        static async Task<IResult> ListSeasonsAsync(IMediator mediator,
            int pageNumber = 1,
            int PageSize = 20)
            => Results.Ok(await mediator.Send(new GetSeasons.Query(pageNumber, PageSize)));

        static async Task<IResult> GetSeasonAsync(int year, IMediator mediator, IScopedErrorService errorService)
            => await mediator.SendQueryAsync(new GetSeasonByYearQuery(year), errorService);
    }
}
