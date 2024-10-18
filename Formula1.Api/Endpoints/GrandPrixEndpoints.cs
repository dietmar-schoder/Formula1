using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class GrandPrixEndpoints
{
    public static void MapGrandPrixEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/grandprix", ListGrandPrixAsync);
        app.MapGet("/api/grandprix/{id:guid}", GetGrandPrixAsync);

        static async Task<IResult> ListGrandPrixAsync(IMediator mediator)
            => Results.Ok(await mediator.Send(new GetGrandPrixQuery()));

        static async Task<IResult> GetGrandPrixAsync(Guid id, IMediator mediator, IScopedErrorService errorService)
            => await mediator.SendQueryAsync(new GetGrandPrixByIdQuery(id), errorService);
    }
}
