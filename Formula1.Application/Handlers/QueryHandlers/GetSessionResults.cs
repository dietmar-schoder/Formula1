using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSessionResults(
    IApplicationDbContext dbContext)
    : IRequestHandler<GetSessionResults.Query, ResultsPaginatedDto<ResultBasicDto>>
{
    protected readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Id, int PageNumber, int PageSize) : IRequest<ResultsPaginatedDto<ResultBasicDto>> { }

    public async Task<ResultsPaginatedDto<ResultBasicDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        var pageSize = Math.Min(query.PageSize, 100);
        var resultDtos = new List<ResultBasicDto>();
        var totalCount = await _dbContext.FORMULA1_Results.Where(d => d.SessionId == query.Id).CountAsync(cancellationToken);
        var results = await _dbContext.FORMULA1_Results
            .AsNoTracking()
            .Where(d => d.SessionId == query.Id)
            .Include(r => r.Driver)
            .Include(r => r.Constructor)
            .OrderBy(r => r.Position)
            .Skip((query.PageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        foreach (var result in results)
        {
            var resultDto = result.Adapt<ResultBasicDto>();
            resultDto.Points = result.Points / 100m;
            resultDto.DriverName = result.Driver.Name;
            resultDto.ConstructorName = result.Constructor.Name;
            resultDtos.Add(resultDto);
        }
        return new ResultsPaginatedDto<ResultBasicDto>(
            resultDtos,
            query.PageNumber,
            pageSize,
            totalCount);
    }
}
