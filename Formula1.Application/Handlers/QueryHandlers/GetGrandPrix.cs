using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetGrandPrix(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetGrandPrix.Query, GrandPrixPaginatedDto<GrandPrixDto>>
{
    public record Query(int PageNumber, int PageSize) : IRequest<GrandPrixPaginatedDto<GrandPrixDto>> { }

    public async Task<GrandPrixPaginatedDto<GrandPrixDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        Log();
        var totalCount = await _dbContext.FORMULA1_GrandPrix.CountAsync(cancellationToken);
        var grandPrix = await _dbContext.FORMULA1_GrandPrix
            .AsNoTracking()
            .OrderBy(e => e.Name)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);
        Log(grandPrix.Count.ToString(), nameof(grandPrix.Count));
        return new GrandPrixPaginatedDto<GrandPrixDto>(
            grandPrix.Adapt<List<GrandPrixDto>>(),
            query.PageNumber,
            query.PageSize,
            totalCount);
    }
}
