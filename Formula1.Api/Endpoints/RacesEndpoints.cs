using Formula1.Application.Commands.ImportCommands;
using Formula1.Application.Queries;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class RacesEndpoints
{
    public static void MapRacesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/races/{id:guid}", GetRaceAsync);
        app.MapPost("/api/races", ImportAllRacesAsync);
        app.MapPost("/api/races/{year:int}", ImportYearRacesAsync);

        static async Task<IResult> GetRaceAsync(Guid id, IMediator mediator)
            => Results.Ok(await mediator.Send(new GetRaceByIdQuery(id)));

        static async Task<IResult> ImportAllRacesAsync(IMediator mediator)
            => Results.Ok(await mediator.Send(new ImportRacesCommand(0)));

        static async Task<IResult> ImportYearRacesAsync(int year, IMediator mediator)
            => Results.Ok(await mediator.Send(new ImportRacesCommand(year)));
    }
}
