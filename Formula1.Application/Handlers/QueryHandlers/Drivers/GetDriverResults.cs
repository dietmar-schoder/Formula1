using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using Formula1.Contracts.Dtos.PaginatedDtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers.Drivers;

public class GetDriverResults(
    IApplicationDbContext dbContext)
    : IRequestHandler<GetDriverResults.Query, ResultsPaginatedDto<DriverResultDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Id, int PageNumber, int PageSize) : IRequest<ResultsPaginatedDto<DriverResultDto>> { }

    public async Task<ResultsPaginatedDto<DriverResultDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        var pageSize = Math.Min(query.PageSize, 100);
        var totalCount = await _dbContext.FORMULA1_Results.Where(d => d.DriverId == query.Id).CountAsync(cancellationToken);
        var resultDtos = await _dbContext.FORMULA1_Results
            .AsNoTracking()
            .Where(d => d.DriverId == query.Id)
            .Include(r => r.Session)
                .ThenInclude(s => s.Race)
                    .ThenInclude(s => s.GrandPrix)
            .Include(r => r.Session)
                .ThenInclude(s => s.Race)
                    .ThenInclude(r => r.Circuit)
            .Include(r => r.Session)
                .ThenInclude(s => s.SessionType)
            .Include(r => r.Constructor)
            .OrderByDescending(r => r.Session.Race.SeasonYear)
                .ThenBy(r => r.Session.Race.Round)
                .ThenBy(r => r.Session.SessionTypeId)
            .Skip((query.PageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(r => DriverResultDto.FromResult(r))
            .ToListAsync(cancellationToken);
        return new ResultsPaginatedDto<DriverResultDto>(
            resultDtos,
            query.PageNumber,
            pageSize,
            totalCount);
    }
}
