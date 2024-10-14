using Formula1.Contracts.Dtos;
using MediatR;

namespace Formula1.Application.Queries;

public class GetResultByIdQuery(Guid id) : IRequest<ResultDto>
{
    public Guid Id { get; set; } = id;
}
