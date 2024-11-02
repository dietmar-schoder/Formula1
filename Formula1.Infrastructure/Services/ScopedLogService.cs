using Formula1.Application.Interfaces.Services;
using System.Runtime.CompilerServices;

namespace Formula1.Infrastructure.Services;

public class ScopedLogService : IScopedLogService
{
    private readonly List<string> _logs = [];
    private readonly List<string> _texts = [];

    public void Log(
        string content = "",
        string var = default,
        [CallerMemberName] string callerMethod = "",
        [CallerFilePath] string callerFile = "",
        [CallerLineNumber] int callerLine = 0)
    {
        var = string.IsNullOrEmpty(var) ? string.Empty : $"{var}: ";
        string className = Path.GetFileNameWithoutExtension(callerFile);
        string logEntry = $"{className}.{callerMethod}() - Line {callerLine} [{var}'{content}']";
        _logs.Add($"{_logs.Count + 1}. {logEntry}");
    }

    public void AddText(string text)
        => _texts.Add(text);

    public List<string> GetLogsAsList()
        => [.. _logs, .. _texts];

    public string ExceptionAsTextBlock(Exception exception)
    {
        AddText(exception.Source);
        AddText(exception.StackTrace);
        return GetLogsAsString(exception.Message);
    }

    public string GetLogsAsString(string title)
        => string.Join("\r\n", [title, string.Empty, .. _logs, string.Empty, .. _texts]);
}
