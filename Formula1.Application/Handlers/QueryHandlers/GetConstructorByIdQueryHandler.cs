using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Formula1.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetConstructorByIdQueryHandler(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetConstructorByIdQuery, ConstructorResultsDto>
{
    public async Task<ConstructorResultsDto> Handle(GetConstructorByIdQuery request, CancellationToken cancellationToken)
    {
        Log(request.Id.ToString(), nameof(request.Id));
        var constructor = await _dbContext.FORMULA1_Constructors
            .AsNoTracking()
            .Include(e => e.Results).ThenInclude(r => r.Session)
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? AddNotFoundError<Constructor>(request.Id.ToString());
        if (constructor is null) { return default; }
        Log(constructor.Id.ToString(), nameof(constructor.Id));
        Log(constructor.Results.Count.ToString(), nameof(constructor.Results.Count));
        constructor.Results = [.. constructor.Results.OrderByDescending(r => r.Session.StartDateTimeUtc)];
        return constructor.Adapt<ConstructorResultsDto>();
    }
}
