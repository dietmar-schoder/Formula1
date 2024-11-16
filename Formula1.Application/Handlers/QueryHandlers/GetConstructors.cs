using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetConstructors(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService),
        IRequestHandler<GetConstructors.Query, ConstructorsPaginatedDto<ConstructorDto>>
{
    public record Query(int PageNumber, int PageSize) : IRequest<ConstructorsPaginatedDto<ConstructorDto>> { }

    public async Task<ConstructorsPaginatedDto<ConstructorDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        var pageSize = Math.Min(query.PageSize, 100);
        var totalCount = await _dbContext.FORMULA1_Constructors.CountAsync(cancellationToken);
        var constructors = await _dbContext.FORMULA1_Constructors
            .AsNoTracking()
            .OrderBy(e => e.Name)
            .Skip((query.PageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        return new ConstructorsPaginatedDto<ConstructorDto>(
            constructors.Adapt<List<ConstructorDto>>(),
            query.PageNumber,
            pageSize,
            totalCount);
    }
}
