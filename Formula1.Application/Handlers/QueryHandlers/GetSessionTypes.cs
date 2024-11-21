using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using Formula1.Contracts.Dtos.PaginatedDtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSessionTypes(IApplicationDbContext dbContext)
    : IRequestHandler<GetSessionTypes.Query, SessionTypesPaginatedDto>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int PageNumber, int PageSize) : IRequest<SessionTypesPaginatedDto> { }

    public async Task<SessionTypesPaginatedDto> Handle(Query query, CancellationToken cancellationToken)
    {
        var totalCount = await _dbContext.FORMULA1_SessionTypes.CountAsync(cancellationToken);
        var sessionTypeDtos = await _dbContext.FORMULA1_SessionTypes
            .AsNoTracking()
            .OrderBy(s => s.Id)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(s => SessionTypeDto.FromSessionType(s))
            .ToListAsync(cancellationToken);
        return new SessionTypesPaginatedDto(
            sessionTypeDtos,
            query.PageNumber,
            query.PageSize,
            totalCount);
    }
}
