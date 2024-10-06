using Formula1.Contracts.Responses;
using Formula1.Domain.Common.Interfaces;
using MediatR;
using System.Reflection;

namespace Formula1.Application.Queries;

public class GetVersionQueryHandler(IDateTimeProvider dateTimeProvider) : IRequestHandler<GetVersionQuery, Alive>
{
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public Task<Alive> Handle(GetVersionQuery request, CancellationToken cancellationToken)
        => Task.FromResult(new Alive
        {
            UtcNow = _dateTimeProvider.UtcNow,
            Version = Assembly.GetEntryAssembly().GetName().Version.ToString()
        });
}
