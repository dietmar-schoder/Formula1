﻿using Formula1.Application.Handlers.QueryHandlers.Races;
using Formula1.Application.Interfaces.Services;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class RacesEndpoints
{
    public static void MapRacesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/races", ListRacesAsync);
        app.MapGet("/api/races/{id:int}", GetRaceAsync);

        //app.MapGet("/api/races/{id:int}/sessions", GetRaceAsync);
        //app.MapGet("/api/races/{id:int}/results", GetRaceAsync);
        //app.MapGet("/api/races/{id:int}/constructors", GetRaceAsync);
        //app.MapGet("/api/races/{id:int}/drivers", GetRaceAsync);

        static async Task<IResult> ListRacesAsync(IMediator mediator, int pageNumber = 1, int PageSize = 20)
            => Results.Ok(await mediator.Send(new GetRaces.Query(pageNumber, PageSize)));

        static async Task<IResult> GetRaceAsync(IMediator mediator, int id)
            => await mediator.SendQueryAsync(new GetRace.Query(id));
    }
}
