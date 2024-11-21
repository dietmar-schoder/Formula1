using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetResult(IApplicationDbContext dbContext)
    : IRequestHandler<GetResult.Query, ResultDto>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Id) : IRequest<ResultDto> { }

    public async Task<ResultDto> Handle(Query request, CancellationToken cancellationToken)
    {
        var result = await _dbContext.FORMULA1_Results
            .AsNoTracking()
            .Include(r => r.Session)
                .ThenInclude(r => r.Race)
                    .ThenInclude(s => s.GrandPrix)
            .Include(r => r.Session)
                .ThenInclude(s => s.Race)
                    .ThenInclude(r => r.Circuit)
            .Include(r => r.Session)
                .ThenInclude(s => s.SessionType)
            .Include(r => r.Driver)
            .Include(r => r.Constructor)
            .FirstOrDefaultAsync(r => r.Id.Equals(request.Id), cancellationToken);
        return result is null ? null : ResultDto.FromResult(result);
    }
}
