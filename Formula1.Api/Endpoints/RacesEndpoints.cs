using Formula1.Api.Extensions;
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

        static async Task<IResult> ListRacesAsync(IMediator mediator)
            => Results.Ok(await mediator.Send(new GetRacesQuery()));

        static async Task<IResult> GetRaceAsync(Guid id, IMediator mediator, IScopedErrorService errorService)
            => await mediator.SendQueryAsync(new GetRaceByIdQuery(id), errorService);
    }
}
