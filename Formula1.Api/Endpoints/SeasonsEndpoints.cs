﻿using Formula1.Application.Handlers.QueryHandlers.Seasons;
using Formula1.Application.Interfaces.Services;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class SeasonsEndpoints
{
    public static void MapSeasonsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/seasons", ListSeasonsAsync);
        app.MapGet("/api/seasons/{year:int}", GetSeasonAsync);
        app.MapGet("/api/seasons/{year:int}/races", GetSeasonRacesAsync);
        app.MapGet("/api/seasons/{year:int}/drivers", GetSeasonDriversAsync);
        app.MapGet("/api/seasons/{year:int}/drivers/{id:int}/results", GetSeasonDriverResultsAsync);
        app.MapGet("/api/seasons/{year:int}/constructors", GetSeasonConstructorsAsync);
        app.MapGet("/api/seasons/{year:int}/constructors/{id:int}/results", GetSeasonConstructorResultsAsync);
        //app.MapGet("/api/seasons/{year:int}/circuits", GetSeasonAsync);
        //app.MapGet("/api/seasons/{year:int}/sessions", GetSeasonAsync);
        //app.MapGet("/api/seasons/{year:int}/results", GetSeasonAsync);

        static async Task<IResult> ListSeasonsAsync(IMediator mediator, int pageNumber = 1, int PageSize = 20)
            => Results.Ok(await mediator.Send(new GetSeasons.Query(pageNumber, PageSize)));

        static async Task<IResult> GetSeasonRacesAsync(IMediator mediator, int year)
            => await mediator.SendQueryAsync(new GetSeasonRaces.Query(year));

        static async Task<IResult> GetSeasonAsync(IMediator mediator, int year)
            => await mediator.SendQueryAsync(new GetSeason.Query(year));

        static async Task<IResult> GetSeasonDriversAsync(IMediator mediator, int year)
            => await mediator.SendQueryAsync(new GetSeasonDrivers.Query(year));

        static async Task<IResult> GetSeasonDriverResultsAsync(IMediator mediator, int year, int id)
            => await mediator.SendQueryAsync(new GetSeasonDriverResults.Query(year, id));

        static async Task<IResult> GetSeasonConstructorsAsync(IMediator mediator, int year)
            => await mediator.SendQueryAsync(new GetSeasonConstructors.Query(year));

        static async Task<IResult> GetSeasonConstructorResultsAsync(IMediator mediator, int year, int id)
            => await mediator.SendQueryAsync(new GetSeasonConstructorResults.Query(year, id));
    }
}
