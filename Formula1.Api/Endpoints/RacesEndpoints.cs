using Formula1.Application.Commands.ImportCommands;
using Formula1.Application.Queries;
using Formula1.Contracts.Requests;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class RacesEndpoints
{
    public static void MapRacesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/races/{id:guid}", GetRaceAsync);
        app.MapPost("/api/races", ImportRacesAsync);

        static async Task<IResult> GetRaceAsync(Guid id, IMediator mediator)
            => Results.Ok(await mediator.Send(new GetRaceByIdQuery(id)));

        static async Task<IResult> ImportRacesAsync(ImportRequest request, IMediator mediator)
            => Results.Ok(await mediator.Send(new ImportRacesCommand(request)));
    }
}
