using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetDriverConstructors(
    IApplicationDbContext dbContext)
    : IRequestHandler<GetDriverConstructors.Query, ConstructorsPaginatedDto<ConstructorDto>>
{
    protected readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Id) : IRequest<ConstructorsPaginatedDto<ConstructorDto>> { }

    public async Task<ConstructorsPaginatedDto<ConstructorDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        var constructors = await _dbContext.FORMULA1_Results
            .AsNoTracking()
            .Where(r => r.DriverId == query.Id)
            .GroupBy(r => r.Constructor)
            .Select(g => g.Key)
            .ToListAsync(cancellationToken);
        constructors = [.. constructors.OrderBy(c => c.Name)];
        return new ConstructorsPaginatedDto<ConstructorDto>(
            constructors.Adapt<List<ConstructorDto>>(),
            1,
            constructors.Count,
            constructors.Count);
    }
}
