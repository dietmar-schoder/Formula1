using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.Dtos;
using Formula1.Contracts.Dtos.PaginatedDtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSessionTypes(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetSessionTypes.Query, SessionTypesPaginatedDto>
{
    public record Query(int PageNumber, int PageSize) : IRequest<SessionTypesPaginatedDto> { }

    public async Task<SessionTypesPaginatedDto> Handle(Query query, CancellationToken cancellationToken)
    {
        Log();
        var totalCount = await _dbContext.FORMULA1_SessionTypes.CountAsync(cancellationToken);
        var sessionTypes = await _dbContext.FORMULA1_SessionTypes
            .AsNoTracking()
            .OrderBy(e => e.Id)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);
        Log(sessionTypes.Count.ToString(), nameof(sessionTypes.Count));
        return new SessionTypesPaginatedDto(
            sessionTypes.Adapt<List<SessionTypeDto>>(),
            query.PageNumber,
            query.PageSize,
            totalCount);
    }
}
