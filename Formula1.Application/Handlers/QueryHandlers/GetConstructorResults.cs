using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using Formula1.Contracts.Dtos.PaginatedDtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetConstructorResults(
    IApplicationDbContext dbContext)
    : IRequestHandler<GetConstructorResults.Query, ResultsPaginatedDto<ConstructorResultDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Id, int PageNumber, int PageSize) : IRequest<ResultsPaginatedDto<ConstructorResultDto>> { }

    public async Task<ResultsPaginatedDto<ConstructorResultDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        var pageSize = Math.Min(query.PageSize, 100);
        var totalCount = await _dbContext.FORMULA1_Results.Where(d => d.ConstructorId == query.Id).CountAsync(cancellationToken);
        var resultDtos = await _dbContext.FORMULA1_Results
            .AsNoTracking()
            .Where(d => d.ConstructorId == query.Id)
            .Include(r => r.Session)
                .ThenInclude(s => s.Race)
                    .ThenInclude(s => s.GrandPrix)
            .Include(r => r.Session)
                .ThenInclude(s => s.Race)
                    .ThenInclude(r => r.Circuit)
            .Include(r => r.Session)
                .ThenInclude(s => s.SessionType)
            .Include(r => r.Driver)
            .OrderByDescending(r => r.Session.Race.SeasonYear)
                .ThenBy(r => r.Session.Race.Round)
                .ThenBy(r => r.Session.SessionTypeId)
            .Skip((query.PageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(r => ConstructorResultDto.FromResult(r))
            .ToListAsync(cancellationToken);
        return new ResultsPaginatedDto<ConstructorResultDto>(
            resultDtos,
            query.PageNumber,
            pageSize,
            totalCount);
    }
}
