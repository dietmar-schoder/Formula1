using Formula1.Contracts.Dtos;
using MediatR;

namespace Formula1.Application.Queries;

public class GetSeasonByYearQuery(int year) : IRequest<SeasonDto>
{
    public int Year { get; set; } = year;
}
