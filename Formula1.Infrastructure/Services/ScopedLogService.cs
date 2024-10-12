using Formula1.Application.Interfaces.Services;
using Formula1.Domain.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;

namespace Formula1.Infrastructure.Services;

public class ScopedLogService : IScopedLogService
{
    private readonly List<string> _logs = [];
    private readonly List<string> _texts = [];

    public void Log(
        string content = "",
        string var = "",
        [CallerMemberName] string callerMethod = "",
        [CallerFilePath] string callerFile = "",
        [CallerLineNumber] int callerLine = 0)
    {
        var = var.IsNullOrEmpty() ? string.Empty : $"{var}: ";
        string className = Path.GetFileNameWithoutExtension(callerFile);
        string logEntry = $"{className}.{callerMethod}() - Line {callerLine} [{var}'{content}']";
        _logs.Add($"{_logs.Count + 1}. {logEntry}");
    }

    public void AddText(string text)
        => _texts.Add(text);

    public List<string> GetLogsAsList()
        => [.. _logs, .. _texts];

    public string GetLogsAsString(string title)
        => string.Join("\r\n", [title, string.Empty, .. _logs, string.Empty, .. _texts]);

    public void ThrowError(int statusCode, string message)
        => throw new UserError(statusCode, message);

    public T ThrowNotFoundError<T>(string key) where T : class
        => throw new UserError(404, $"Resource {typeof(T).Name} not found for '{key}'.");

    public void ThrowException()
        => throw new Exception();
}
