using Formula1.Application.Commands.ImportCommands;
using Formula1.Application.Queries;
using Formula1.Contracts.Requests;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class DriversEndpoints
{
    public static void MapDriversEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/drivers", ListDriversAsync);
        //app.MapGet("/api/drivers/{id:guid}", GetDriverAsync);
        app.MapPost("/api/drivers", ImportDriversAsync);

        static async Task<IResult> ListDriversAsync(IMediator mediator)
            => Results.Ok(await mediator.Send(new GetDriversQuery()));

        //static async Task<IResult> GetDriverAsync(Guid id, IMediator mediator)
        //    => Results.Ok(await mediator.Send(new GetGetDriverByIdQuery(id)));

        static async Task<IResult> ImportDriversAsync(ImportRequest request, IMediator mediator)
            => Results.Ok(await mediator.Send(new ImportDriversCommand(request)));
    }
}
