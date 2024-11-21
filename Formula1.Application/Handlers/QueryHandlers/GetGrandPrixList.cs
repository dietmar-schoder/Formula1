using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using Formula1.Contracts.Dtos.PaginatedDtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetGrandPrixList(IApplicationDbContext dbContext)
    : IRequestHandler<GetGrandPrixList.Query, GrandPrixPaginatedDto>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int PageNumber, int PageSize) : IRequest<GrandPrixPaginatedDto> { }

    public async Task<GrandPrixPaginatedDto> Handle(Query query, CancellationToken cancellationToken)
    {
        var totalCount = await _dbContext.FORMULA1_GrandPrix.CountAsync(cancellationToken);
        var grandPrixDtos = await _dbContext.FORMULA1_GrandPrix
            .AsNoTracking()
            .OrderBy(g => g.Name)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(g => GrandPrixDto.FromGrandPrix(g))
            .ToListAsync(cancellationToken);
        return new GrandPrixPaginatedDto(
            grandPrixDtos,
            query.PageNumber,
            query.PageSize,
            totalCount);
    }
}
