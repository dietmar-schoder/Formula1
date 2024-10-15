using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Formula1.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetResultByIdQueryHandler(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetResultByIdQuery, ResultDto>
{
    public async Task<ResultDto> Handle(GetResultByIdQuery request, CancellationToken cancellationToken)
    {
        Log(request.Id.ToString(), nameof(request.Id));
        var result = await _dbContext.FORMULA1_Results
            .AsNoTracking()
            .Include(r => r.Session)
            .Include(r => r.Driver)
            .Include(r => r.Constructor)
            .FirstOrDefaultAsync(r => r.Id.Equals(request.Id), cancellationToken)
            ?? AddNotFoundError<Result>(request.Id.ToString());
        if (result is null) { return default; }
        Log(result.Id.ToString(), nameof(result.Id));
        return result.Adapt<ResultDto>();
    }
}
