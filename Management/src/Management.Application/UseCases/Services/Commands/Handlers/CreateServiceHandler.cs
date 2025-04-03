using Management.Domain.Entities;
using Management.Domain.Repositories;
using Management.Shared.DTOs;
using MediatR;

namespace Management.Application.UseCases.Services.Commands.Handlers
{
    public sealed class CreateServiceHandler : IRequestHandler<CreateService, ServiceDto?>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IRepository _repository;

        public CreateServiceHandler(IServiceRepository serviceRepository, IRepository repository)
        {
            _serviceRepository = serviceRepository;
            _repository = repository;
        }

        public async Task<ServiceDto?> Handle(CreateService command, CancellationToken cancellationToken)
        {
            var service = Service.Create(command.Name);
            await _serviceRepository.AddAsync(service);
            await _repository.SaveChangesAsync();

            return service.AsDto();
        }
    }
}
