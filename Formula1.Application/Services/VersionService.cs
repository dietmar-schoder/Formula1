using Formula1.Contracts.Responses;
using Formula1.Domain.Common.Interfaces;
using System.Reflection;

namespace Formula1.Application.Services;

public class VersionService(IDateTimeProvider dateTimeProvider) : IVersionService
{
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public Alive GetVersion()
        => new()
        {
            UtcNow = _dateTimeProvider.UtcNow,
            Version = Assembly.GetEntryAssembly().GetName().Version.ToString()
        };
}
