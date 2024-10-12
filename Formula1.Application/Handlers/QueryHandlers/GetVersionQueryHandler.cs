using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Responses;
using Formula1.Domain.Common.Interfaces;
using MediatR;
using System.Reflection;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetVersionQueryHandler(
    IDateTimeProvider dateTimeProvider,
    IScopedLogService logService)
    : IRequestHandler<GetVersionQuery, AliveResponse>
{
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    private readonly IScopedLogService _logService = logService;

    public Task<AliveResponse> Handle(GetVersionQuery request, CancellationToken cancellationToken)
    {
        _logService.Log();
        var alive = new AliveResponse
        {
            UtcNow = _dateTimeProvider.UtcNow,
            Version = Assembly.GetEntryAssembly().GetName().Version.ToString()
        };
        _logService.Log(alive.Version, nameof(alive.Version));
        return Task.FromResult(alive);
    }
}
