namespace Formula1.Api.Endpoints;

public static class GrandPrixEndpoints
{
    public static void MapGrandPrixEndpoints(this IEndpointRouteBuilder app)
    {
        //app.MapGet("/api/circuits", ListGrandPrixAsync);
        //app.MapGet("/api/circuits/{id:guid}", GetGrandPrixAsync);

        //static async Task<IResult> ListGrandPrixAsync(IMediator mediator)
        //    => Results.Ok(await mediator.Send(new GetGrandPrixQuery()));

        //static async Task<IResult> GetCircuitAsync(Guid id, IMediator mediator)
        //    => Results.Ok(await mediator.Send(new GetGrandPrixByIdQuery(id)));
    }
}
