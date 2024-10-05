using Formula1.Contracts.Dtos;
using Formula1.Contracts.Services;
using System.Net.Http.Json;

namespace Formula1.Infrastructure.Services;

public class OpenF1Service(HttpClient client) : IOpenF1Service
{
    private readonly HttpClient _client = client;

    public string BaseAddress => _client.BaseAddress?.AbsoluteUri;

    public static string GetJobDataUrl()
        //=> $"/v1/api/jobs/gb/search/1?app_id=57a12a98&app_key=a174af374d525386c8275716ab096834&what_and=.net,c%23&content-type=application/json&salary_min=40000&salary_max=120000&where=manchester&distance=220&max_days_old=1&results_per_page=100000";
        => $"/v1/api/jobs/gb/search/1?app_id=57a12a98&app_key=a174af374d525386c8275716ab096834&what_and=.net,c%23&content-type=application/json&salary_min=40000&salary_max=120000&where=london&distance=200&max_days_old=1&results_per_page=100000";

    public async Task<JobData_OLD> ListJobsAsync()
    {
        var response = await _client.GetAsync(GetJobDataUrl());
        var contentType = response.Content.Headers.ContentType;
        if (contentType != null && contentType.CharSet == "utf8")
        {
            contentType.CharSet = "utf-8";
        }
        var jobData = await response.Content.ReadFromJsonAsync<JobData_OLD>();
        return jobData;
    }
}
