﻿using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Branches
{
    public sealed record DeleteBranch(Guid branchId) : IRequest;
}
