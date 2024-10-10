using Formula1.Application.Queries;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class CircuitsEndpoints
{
    public static void MapCircuitsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/circuits", ListCircuitsAsync);
        app.MapGet("/api/circuits/{id:guid}", GetCircuitAsync);

        static async Task<IResult> ListCircuitsAsync(IMediator mediator)
            => Results.Ok(await mediator.Send(new GetCircuitsQuery()));

        static async Task<IResult> GetCircuitAsync(Guid id, IMediator mediator)
            => Results.Ok(await mediator.Send(new GetCircuitByIdQuery(id)));
    }
}
