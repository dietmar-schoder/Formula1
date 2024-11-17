using Formula1.Application.Handlers.QueryHandlers;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class ResultsEndpoints
{
    public static void MapResultsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/results", ListResultsAsync);
        //app.MapGet("/api/results/{id:int}", GetResultAsync);

        static async Task<IResult> ListResultsAsync(IMediator mediator, int pageNumber = 1, int PageSize = 20)
            => Results.Ok(await mediator.Send(new GetResults.Query(pageNumber, PageSize)));

        //static async Task<IResult> GetResultAsync(int id, IMediator mediator, IScopedErrorService errorService)
        //    => await mediator.SendQueryAsync(new GetResultByIdQuery(id), errorService);
    }
}
