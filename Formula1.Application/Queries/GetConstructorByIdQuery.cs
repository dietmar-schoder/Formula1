using Formula1.Contracts.Dtos;
using MediatR;

namespace Formula1.Application.Queries;

public class GetConstructorByIdQuery(Guid id) : IRequest<ConstructorResultsDto>
{
    public Guid Id { get; set; } = id;
}
