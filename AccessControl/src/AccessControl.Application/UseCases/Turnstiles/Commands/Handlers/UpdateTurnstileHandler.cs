﻿using AccessControl.Domain.DTOs;
using AccessControl.Domain.DTOs.Mappers;
using AccessControl.Domain.Repositories;
using FClub.Backend.Common.Exceptions;
using MediatR;

namespace AccessControl.Application.UseCases.Turnstiles.Commands.Handlers
{
    public sealed class UpdateTurnstileHandler : IRequestHandler<UpdateTurnstile, TurnstileDto?>
    {
        private readonly ITurnstileRepository _turnstileRepository;
        private readonly IRepository _repository;

        public UpdateTurnstileHandler(ITurnstileRepository turnstileRepository, IRepository repository)
        {
            _turnstileRepository = turnstileRepository;
            _repository = repository;
        }

        public async Task<TurnstileDto?> Handle(UpdateTurnstile command, CancellationToken cancellationToken)
        {
            var (id, name, isMain, branchId, serviceId) = command;

            var turnstile = await _turnstileRepository.GetAsync(id)
                ?? throw new NotFoundException($"Cannot find turnstile {id}");

            turnstile.UpdateDetails(name, branchId, serviceId, isMain);
            await _repository.SaveChangesAsync();

            return turnstile.AsDto();
        }
    }
}
