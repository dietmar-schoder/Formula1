﻿using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers.Seasons;

public class GetSeasonConstructorResults(IApplicationDbContext dbContext)
    : IRequestHandler<GetSeasonConstructorResults.Query, List<SeasonConstructorResultDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Year, int ConstructorId) : IRequest<List<SeasonConstructorResultDto>> { }

    public async Task<List<SeasonConstructorResultDto>> Handle(Query query, CancellationToken cancellationToken)
        => await _dbContext.FORMULA1_Results
            .Where(r => r.ConstructorId == query.ConstructorId &&
                        r.Session.Race.SeasonYear == query.Year)
            .Include(r => r.Driver)
            .Include(r => r.Session)
                .ThenInclude(s => s.Race)
                    .ThenInclude(r => r.GrandPrix)
            .Include(r => r.Session)
                .ThenInclude(s => s.Race)
                    .ThenInclude(r => r.Circuit)
            .Include(r => r.Session)
                .ThenInclude(s => s.SessionType)
            .OrderBy(r => r.Session.Race.Round)
                .ThenByDescending(r => r.Session.SessionTypeId)
            .Select(r => SeasonConstructorResultDto.FromResult(r))
            .ToListAsync(cancellationToken);
}
