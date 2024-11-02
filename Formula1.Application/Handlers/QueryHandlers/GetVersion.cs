using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.Responses;
using Formula1.Domain.Common.Interfaces;
using MediatR;
using System.Reflection;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetVersion(
    IDateTimeProvider dateTimeProvider,
    IScopedLogService logService)
    : IRequestHandler<GetVersion.Query, AliveResponse>
{
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    private readonly IScopedLogService _logService = logService;

    public record Query : IRequest<AliveResponse> { }

    public Task<AliveResponse> Handle(Query request, CancellationToken cancellationToken)
    {
        _logService.Log();
        var assembly = Assembly.GetEntryAssembly().GetName();
        var alive = new AliveResponse(assembly.Name, _dateTimeProvider.UtcNow, assembly.Version.ToString());
        _logService.Log(alive.Version, nameof(alive.Version));
        return Task.FromResult(alive);
    }
}
