namespace Formula1.Application.Interfaces.Services;

public interface IExceptionService
{
    Task HandleExceptionAsync(Exception exception);
}
