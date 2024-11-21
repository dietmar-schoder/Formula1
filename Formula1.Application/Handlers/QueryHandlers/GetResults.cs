using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using Formula1.Contracts.Dtos.PaginatedDtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetResults(IApplicationDbContext dbContext)
    : IRequestHandler<GetResults.Query, ResultsPaginatedDto<ResultDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int PageNumber, int PageSize) : IRequest<ResultsPaginatedDto<ResultDto>> { }

    public async Task<ResultsPaginatedDto<ResultDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        var pageSize = Math.Min(query.PageSize, 100);
        var totalCount = await _dbContext.FORMULA1_Results.CountAsync(cancellationToken);
        var resultDtos = await _dbContext.FORMULA1_Results
            .AsNoTracking()
            .Include(r => r.Session)
                .ThenInclude(s => s.Race)
                    .ThenInclude(s => s.GrandPrix)
            .Include(r => r.Session)
                .ThenInclude(s => s.Race)
                    .ThenInclude(r => r.Circuit)
            .Include(r => r.Session)
                .ThenInclude(s => s.SessionType)
            .Include(r => r.Driver)
            .Include(r => r.Constructor)
            .OrderByDescending(r => r.Session.Race.SeasonYear)
                .ThenBy(r => r.Session.Race.Round)
                .ThenBy(r => r.Session.SessionTypeId)
                .ThenBy(r => r.Position)
            .Skip((query.PageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(r => ResultDto.FromResult(r))
            .ToListAsync(cancellationToken);
        return new ResultsPaginatedDto<ResultDto>(
            resultDtos,
            query.PageNumber,
            pageSize,
            totalCount);
    }
}
