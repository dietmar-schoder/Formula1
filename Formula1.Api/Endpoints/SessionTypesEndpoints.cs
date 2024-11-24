using Formula1.Application.Handlers.QueryHandlers.Sessions;
using Formula1.Domain.Entities;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class SessionTypesEndpoints
{
    public static void MapSessionTypesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/sessiontypes", ListSessionTypesAsync);

        static async Task<IResult> ListSessionTypesAsync(HttpContext httpContext, IMediator mediator, int pageNumber = 1, int PageSize = 20)
        {
            var paginatedResult = await mediator.Send(new GetSessionTypes.Query(pageNumber, PageSize));
            var acceptHeader = httpContext.Request.Headers.Accept.ToString();
            if (acceptHeader.Contains("text/csv"))
            {
                var csvContent = "SessionType,Description\n";
                foreach (var sessionType in paginatedResult.SessionTypes)
                {
                    csvContent += $"{sessionType.Id},{sessionType.Description}\n";
                }
                return Results.Text(csvContent, "text/csv");
            }
            return Results.Ok(paginatedResult);
        }
    }
}
