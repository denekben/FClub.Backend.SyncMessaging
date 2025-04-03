using FClub.Backend.Common.Exceptions;
using Management.Domain.Repositories;
using Management.Shared.DTOs;
using MediatR;

namespace Management.Application.UseCases.Services.Commands.Handlers
{
    public sealed class UpdateServiceHandler : IRequestHandler<UpdateService, ServiceDto?>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IRepository _repository;

        public UpdateServiceHandler(IServiceRepository serviceRepository, IRepository repository)
        {
            _serviceRepository = serviceRepository;
            _repository = repository;
        }

        public async Task<ServiceDto?> Handle(UpdateService command, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetAsync(command.Id)
                ?? throw new NotFoundException($"Cannot find service {command.Id}");

            service.UpdateDetails(command.Name);
            await _serviceRepository.UpdateAsync(service);
            await _repository.SaveChangesAsync();

            return service.AsDto();
        }
    }
}
