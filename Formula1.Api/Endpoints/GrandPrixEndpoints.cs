using Formula1.Application.Commands.ImportCommands;
using Formula1.Contracts.Requests;

//using Formula1.Application.Queries;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class GrandPrixEndpoints
{
    public static void MapGrandPrixEndpoints(this IEndpointRouteBuilder app)
    {
        //app.MapGet("/api/circuits", ListGrandPrixAsync);
        //app.MapGet("/api/circuits/{id:guid}", GetGrandPrixAsync);
        app.MapPost("/api/grandprix", ImportGrandPrixAsync);

        //static async Task<IResult> ListGrandPrixAsync(IMediator mediator)
        //    => Results.Ok(await mediator.Send(new GetGrandPrixQuery()));

        //static async Task<IResult> GetCircuitAsync(Guid id, IMediator mediator)
        //    => Results.Ok(await mediator.Send(new GetGrandPrixByIdQuery(id)));

        static async Task<IResult> ImportGrandPrixAsync(ImportRequest request, IMediator mediator)
            => Results.Ok(await mediator.Send(new ImportGrandPrixCommand(request)));
    }
}
