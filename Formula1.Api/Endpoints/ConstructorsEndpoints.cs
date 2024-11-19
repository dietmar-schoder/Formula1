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
        app.MapGet("/api/constructors/{id:int}", GetConstructorAsync);
        app.MapGet("/api/constructors/{id:int}/drivers", GetConstructorDriversAsync);
        app.MapGet("/api/constructors/{id:int}/results", GetConstructorResultsAsync);
        //app.MapGet("/api/constructors/{id:int}/races", GetConstructorAsync);
        //app.MapGet("/api/constructors/{id:int}/seasons", GetConstructorAsync);

        static async Task<IResult> ListConstructorsAsync(IMediator mediator, int pageNumber = 1, int PageSize = 20)
            => Results.Ok(await mediator.Send(new GetConstructors.Query(pageNumber, PageSize)));

        static async Task<IResult> GetConstructorAsync(int id, IMediator mediator)
            => await mediator.SendQueryAsync(new GetConstructor.Query(id));

        static async Task<IResult> GetConstructorResultsAsync(IMediator mediator, int id, int pageNumber = 1, int pageSize = 20)
            => await mediator.SendQueryAsync(new GetConstructorResults.Query(id, pageNumber, pageSize));

        static async Task<IResult> GetConstructorDriversAsync(IMediator mediator, int id)
            => await mediator.SendQueryAsync(new GetConstructorDrivers.Query(id));
    }
}
