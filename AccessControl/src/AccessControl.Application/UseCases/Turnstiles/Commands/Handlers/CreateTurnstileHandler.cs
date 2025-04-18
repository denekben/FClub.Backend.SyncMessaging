﻿using AccessControl.Domain.DTOs;
using AccessControl.Domain.DTOs.Mappers;
using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;
using FClub.Backend.Common.Exceptions;
using MediatR;

namespace AccessControl.Application.UseCases.Turnstiles.Commands.Handlers
{
    public sealed class CreateTurnstileHandler : IRequestHandler<CreateTurnstile, TurnstileDto?>
    {
        private readonly ITurnstileRepository _turnstileRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IRepository _repository;

        public CreateTurnstileHandler(
            ITurnstileRepository turnstileRepository, IBranchRepository branchRepository, IRepository repository)
        {
            _turnstileRepository = turnstileRepository;
            _branchRepository = branchRepository;
            _repository = repository;
        }

        public async Task<TurnstileDto?> Handle(CreateTurnstile command, CancellationToken cancellationToken)
        {
            var (name, isMain, branchId, serviceId) = command;

            var branch = await _branchRepository.GetAsync(branchId, BranchIncludes.ServicesBranches);
            if (branch == null)
                throw new NotFoundException($"Cannot find branch {branchId}");
            if (!branch.ServiceBranches.Any(sb => sb.ServiceId == serviceId))
                throw new NotFoundException($"Cannot find service {serviceId}");

            var turnstile = Turnstile.Create(name, branchId, serviceId, isMain);
            await _turnstileRepository.AddAsync(turnstile);
            await _repository.SaveChangesAsync();

            return turnstile.AsDto();
        }
    }
}
