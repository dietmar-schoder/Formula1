namespace Formula1.Contracts.Responses;

public class ErrorResponse(string error, List<string> logs = default)
{
    public string Error { get; set; } = error;

    public List<string> Logs { get; set; } = logs;
}
