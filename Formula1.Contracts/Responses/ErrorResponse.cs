namespace Formula1.Contracts.Responses;

public class ErrorResponse(string error)
{
    public string Error { get; set; } = error;
}
