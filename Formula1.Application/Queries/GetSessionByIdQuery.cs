﻿using Formula1.Contracts.Dtos;
using MediatR;

namespace Formula1.Application.Queries;

public class GetSessionByIdQuery(Guid id) : IRequest<SessionDto>
{
    public Guid Id { get; set; } = id;
}
