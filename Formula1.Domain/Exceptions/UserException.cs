namespace Formula1.Domain.Exceptions;

public class UserException(int StatusCode, string message)
    : Exception(message)
{
    public int StatusCode { get; set; } = StatusCode;
}
