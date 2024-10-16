using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class ResultsEndpoints
{
    public static void MapResultsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/results", ListResultsAsync);
        app.MapGet("/api/results/{id:guid}", GetResultAsync);

        static async Task<IResult> ListResultsAsync(IMediator mediator)
            => Results.Ok(await mediator.Send(new GetResultsQuery()));

        static async Task<IResult> GetResultAsync(Guid id, IMediator mediator, IScopedErrorService errorService)
            => await mediator.SendQueryAsync(new GetResultByIdQuery(id), errorService);
    }
}
