using Formula1.Application.Services;

namespace Formula1.Api.Endpoints;

public static class DataImportEndpoints
{
    public static void MapDataImportEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/imports", ListJobsAsHtmlAsync);

        static async Task<IResult> ListJobsAsHtmlAsync(IDataImportService dataImportService)
            => Results.Text(await dataImportService.ListJobsAsHtmlAsync(), "text/html");
    }
}
