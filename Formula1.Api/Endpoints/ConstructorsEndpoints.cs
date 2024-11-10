using Formula1.Application.Handlers.QueryHandlers;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class ConstructorsEndpoints
{
    public static void MapConstructorsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/constructors", ListConstructorsAsync);
        app.MapGet("/api/constructors/{id:guid}", GetConstructorAsync);

        app.MapGet("/api/constructors/{id:guid}/results", GetConstructorAsync);
        app.MapGet("/api/constructors/{id:guid}/drivers", GetConstructorAsync);
        app.MapGet("/api/constructors/{id:guid}/races", GetConstructorAsync);
        app.MapGet("/api/constructors/{id:guid}/seasons", GetConstructorAsync);

        static async Task<IResult> ListConstructorsAsync(
            IMediator mediator,
            int pageNumber = 1,
            int PageSize = 20)
            => Results.Ok(await mediator.Send(new GetConstructors.Query(pageNumber, PageSize)));

        static async Task<IResult> GetConstructorAsync(Guid id, IMediator mediator, IScopedErrorService errorService)
            => await mediator.SendQueryAsync(new GetConstructorByIdQuery(id), errorService);
    }
}
