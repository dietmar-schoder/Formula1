using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSessionResults(IApplicationDbContext dbContext)
    : IRequestHandler<GetSessionResults.Query, List<SessionResultDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Id, int PageNumber, int PageSize) : IRequest<List<SessionResultDto>> { }

    public async Task<List<SessionResultDto>> Handle(Query query, CancellationToken cancellationToken)
        => await _dbContext.FORMULA1_Results
            .AsNoTracking()
            .Where(d => d.SessionId == query.Id)
            .Include(r => r.Driver)
            .Include(r => r.Constructor)
            .OrderBy(r => r.Position)
            .Select(r => SessionResultDto.FromResult(r))
            .ToListAsync(cancellationToken);
}
