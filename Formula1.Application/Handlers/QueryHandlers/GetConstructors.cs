using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using Formula1.Contracts.Dtos.PaginatedDtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetConstructors(IApplicationDbContext dbContext)
    : IRequestHandler<GetConstructors.Query, ConstructorsPaginatedDto<ConstructorDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int PageNumber, int PageSize) : IRequest<ConstructorsPaginatedDto<ConstructorDto>> { }

    public async Task<ConstructorsPaginatedDto<ConstructorDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        var pageSize = Math.Min(query.PageSize, 100);
        var totalCount = await _dbContext.FORMULA1_Constructors.CountAsync(cancellationToken);
        var constructorDtos = await _dbContext.FORMULA1_Constructors
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .Skip((query.PageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(c => ConstructorDto.FromConstructor(c))
            .ToListAsync(cancellationToken);
        return new ConstructorsPaginatedDto<ConstructorDto>(
            constructorDtos,
            query.PageNumber,
            pageSize,
            totalCount);
    }
}
