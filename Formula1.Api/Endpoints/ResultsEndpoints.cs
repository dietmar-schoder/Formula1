using Formula1.Application.Handlers.QueryHandlers;
using Formula1.Application.Interfaces.Services;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class ResultsEndpoints
{
    public static void MapResultsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/results", ListResultsAsync);
        app.MapGet("/api/results/{id:int}", GetResultAsync);

        static async Task<IResult> ListResultsAsync(IMediator mediator, int pageNumber = 1, int PageSize = 20)
            => Results.Ok(await mediator.Send(new GetResults.Query(pageNumber, PageSize)));

        static async Task<IResult> GetResultAsync(IMediator mediator, int id)
            => await mediator.SendQueryAsync(new GetResult.Query(id));
    }
}
