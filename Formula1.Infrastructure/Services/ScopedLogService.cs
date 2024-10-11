using Formula1.Application.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;

namespace Formula1.Infrastructure.Services;

public class ScopedLogService : IScopedLogService
{
    private readonly Queue<string> _logQueue = new();
    
    public void Log(
        string message = "",
        string var = "",
        [CallerMemberName] string callerMethod = "",
        [CallerFilePath] string callerFile = "",
        [CallerLineNumber] int callerLine = 0)
    {
        var = var.IsNullOrEmpty() ? string.Empty : $"{var}: "; 
        string className = Path.GetFileNameWithoutExtension(callerFile);
        string logEntry = $"{className}.{callerMethod}() - Line {callerLine} [{var}'{message}']";
        _logQueue.Enqueue(logEntry);
    }

    public List<string> GetLogs() => [.. _logQueue];
}
