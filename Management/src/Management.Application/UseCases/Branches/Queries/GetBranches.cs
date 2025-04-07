﻿using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.Branches.Queries
{
    public sealed record GetBranches : IRequest<List<BranchDto>?>;
}
