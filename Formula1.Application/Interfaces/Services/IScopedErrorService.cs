namespace Formula1.Application.Interfaces.Services;

public interface IScopedErrorService
{
    void AddError(string message);
    
    void AddErrorIf(bool condition, string message);

    Task ReturnErrorsIfAny(int statusCode = 400);

    Task ReturnError(string message, int statusCode = 400);

    Task<T> ReturnNotFoundErrorAsync<T>(string key) where T : class;

    Task HandleExceptionInDevelopmentAsync(Exception exception);

    Task HandleExceptionInProductionAsync(Exception exception);
}
