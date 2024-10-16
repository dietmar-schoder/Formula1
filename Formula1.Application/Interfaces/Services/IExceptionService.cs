namespace Formula1.Application.Interfaces.Services;

public interface IExceptionService
{
    Task HandleExceptionInDevelopmentAsync(Exception exception);

    Task HandleExceptionInProductionAsync(Exception exception);
}
