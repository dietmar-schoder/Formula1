using Formula1.Application.Commands.ImportCommands;
using Formula1.Application.Queries;
using Formula1.Contracts.Requests;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class ConstructorsEndpoints
{
    public static void MapConstructorsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/constructors", ListConstructorsAsync);
        app.MapGet("/api/constructors/{id:guid}", GetConstructorAsync);
        app.MapPost("/api/constructors", ImportConstructorsAsync);

        static async Task<IResult> ListConstructorsAsync(IMediator mediator)
            => Results.Ok(await mediator.Send(new GetConstructorsQuery()));

        static async Task<IResult> GetConstructorAsync(Guid id, IMediator mediator)
            => Results.Ok(await mediator.Send(new GetConstructorByIdQuery(id)));

        static async Task<IResult> ImportConstructorsAsync(ImportRequest request, IMediator mediator)
            => Results.Ok(await mediator.Send(new ImportConstructorsCommand(request)));
    }
}
