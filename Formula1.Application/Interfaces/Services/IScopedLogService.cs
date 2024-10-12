using System.Runtime.CompilerServices;

namespace Formula1.Application.Interfaces.Services;

public interface IScopedLogService
{
    void Log(
        string content = "",
        string var = "",
        [CallerMemberName] string callerMethod = "",
        [CallerFilePath] string callerFile = "",
        [CallerLineNumber] int callerLine = 0);

    List<string> GetLogs();
}
