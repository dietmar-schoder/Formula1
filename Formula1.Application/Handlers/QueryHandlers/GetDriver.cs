using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetDriver(IApplicationDbContext dbContext)
    : IRequestHandler<GetDriver.Query, DriverDto>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Id) : IRequest<DriverDto> { }

    public async Task<DriverDto> Handle(Query query, CancellationToken cancellationToken)
    {
        var driver = await _dbContext.FORMULA1_Drivers
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == query.Id, cancellationToken);
        return driver is null ? null : DriverDto.FromDriver(driver);
    }
}
