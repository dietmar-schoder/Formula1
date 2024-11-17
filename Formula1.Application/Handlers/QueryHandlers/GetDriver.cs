using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetDriver(IApplicationDbContext dbContext)
    : IRequestHandler<GetDriver.Query, DriverDto>
{
    protected readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Id) : IRequest<DriverDto> { }

    public async Task<DriverDto> Handle(Query query, CancellationToken cancellationToken)
        => (await _dbContext.FORMULA1_Drivers
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == query.Id, cancellationToken))?
            .Adapt<DriverDto>();
}
