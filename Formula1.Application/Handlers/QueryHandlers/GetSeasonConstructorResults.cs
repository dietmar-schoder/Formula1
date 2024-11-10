﻿using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSeasonConstructorResults(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService),
        IRequestHandler<GetSeasonConstructorResults.Query, ResultsPaginatedDto>
{
    public record Query(int Year, Guid ConstructorId) : IRequest<ResultsPaginatedDto> { }

    public async Task<ResultsPaginatedDto> Handle(Query query, CancellationToken cancellationToken)
    {
        var results = await _dbContext.FORMULA1_Results
            .Where(r => r.ConstructorId == query.ConstructorId &&
                        r.Session.Race.SeasonYear == query.Year)
            .Include(r => r.Constructor)
            .Include(r => r.Driver)
            .Include(r => r.Session)
                .ThenInclude(s => s.Race)
                    .ThenInclude(r => r.GrandPrix)
            .Include(r => r.Session)
                .ThenInclude(s => s.SessionType)
            .OrderBy(r => r.Session.Race.Round)
                .ThenBy(r => r.Position)
            .ToListAsync(cancellationToken);
        return new ResultsPaginatedDto(
            results.Adapt<List<ResultDto>>(),
            1,
            results.Count,
            results.Count);
    }
}