using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetConstructorsQueryHandler(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetConstructorsQuery, List<ConstructorBasicDto>>
{
    public async Task<List<ConstructorBasicDto>> Handle(GetConstructorsQuery request, CancellationToken cancellationToken)
    {
        Log();
        var constructors = await _dbContext.FORMULA1_Constructors
            .AsNoTracking()
            .OrderBy(e => e.Name)
            .ToListAsync(cancellationToken);
        Log(constructors.Count.ToString(), nameof(constructors.Count));
        return constructors.Adapt<List<ConstructorBasicDto>>();
    }
}
