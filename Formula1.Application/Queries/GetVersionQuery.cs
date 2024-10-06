using Formula1.Contracts.Responses;
using MediatR;

namespace Formula1.Application.Queries;

public class GetVersionQuery : IRequest<Alive> { }
