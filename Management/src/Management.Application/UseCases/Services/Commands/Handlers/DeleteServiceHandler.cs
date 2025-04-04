﻿using FClub.Backend.Common.Exceptions;
using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.Services.Commands.Handlers
{
    public sealed class DeleteServiceHandler : IRequestHandler<DeleteService>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IRepository _repository;

        public DeleteServiceHandler(IServiceRepository serviceRepository, IRepository repository)
        {
            _serviceRepository = serviceRepository;
            _repository = repository;
        }

        public async Task Handle(DeleteService command, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetAsync(command.Id)
                ?? throw new NotFoundException($"Cannot find service {command.Id}");
            await _serviceRepository.DeleteAsync(command.Id);
            await _repository.SaveChangesAsync();
        }
    }
}
