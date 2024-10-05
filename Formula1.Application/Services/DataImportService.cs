using Formula1.Contracts.Services;
using System.Text;

namespace Formula1.Application.Services;

public class DataImportService(IOpenF1Service openF1Service) : IDataImportService
{
    private readonly IOpenF1Service _openF1Service = openF1Service;

    public async Task<string> ListJobsAsHtmlAsync()
    {
        var jobs = await _openF1Service.ListJobsAsync();
        var html = new StringBuilder();
        //foreach (var job in jobs)
        //{
        //    html.Append($"<tr>" +
        //        $"<td>{job.created}</td>" +
        //        $"<td>{job.company.display_name}</td>" +
        //        $"<td>{job.title}</td>" +
        //        $"<td>{job.location.display_name}</td>" +
        //        $"<td>{ToMinMaxK(job.salary_min, job.salary_max)}</td>" +
        //        $"<td><a href={job.redirect_url} target=_blank>Link</a></td>" +
        //        $"</tr>");
        //}
        return $"<!DOCTYPE html><html lang=\"en\"><body><table>{html}</table></body></html>";

        static string ToMinMaxK(double amountMin, double amountMax)
            => $"{ToK(amountMin)}/{ToK(amountMax)}k";

        static string ToK(double amount)
            => $"{(int)Math.Round(amount / 1_000, 0)}";
    }
}
