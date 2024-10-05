namespace Formula1.Application.Services;

public interface IDataImportService
{
    Task<string> ListJobsAsHtmlAsync();
}
