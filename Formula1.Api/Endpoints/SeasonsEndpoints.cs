﻿using Formula1.Application.Commands.ImportCommands;
using Formula1.Application.Queries;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class SeasonsEndpoints
{
    public static void MapSeasonsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/seasons", ListSeasonsAsync);
        app.MapGet("/api/seasons/{year:int}", GetSeasonAsync);
        app.MapPost("/api/seasons", ImportSeasonsAsync);

        static async Task<IResult> ListSeasonsAsync(IMediator mediator)
            => Results.Ok(await mediator.Send(new GetSeasonsQuery()));

        static async Task<IResult> GetSeasonAsync(int year, IMediator mediator)
            => Results.Ok(await mediator.Send(new GetSeasonByYearQuery(year)));

        static async Task<IResult> ImportSeasonsAsync(IMediator mediator)
            => Results.Ok(await mediator.Send(new ImportSeasonsCommand()));
    }
}