using Management.Domain.Entities;
using Management.Domain.Repositories;
using Management.Shared.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Management.Application.UseCases.Clients.Commands.Handlers
{
    public sealed class CreateClientHandler : IRequestHandler<CreateClient, ClientDto>
    {
        private readonly ILogger<CreateClientHandler> _logger;
        private readonly IClientRepository _clientRepository;
        private readonly IRepository _repository;

        public CreateClientHandler(ILogger<CreateClientHandler> logger, IClientRepository clientRepository, IRepository repository)
        {
            _logger = logger;
            _clientRepository = clientRepository;
            _repository = repository;
        }

        public async Task<ClientDto> Handle(CreateClient command, CancellationToken cancellationToken)
        {
            var (firstName, secondName, patronymic, phone, email, allowEntry, allowNotifications, membershipId) = command;

            var client = Client.Create(firstName, secondName, patronymic, phone, email, allowEntry, allowNotifications, membershipId);

            await _clientRepository.AddAsync(client);
            await _repository.SaveChangesAsync();
            _logger.LogInformation($"Client {client.Id} created");

            return client.AsDto();
        }
    }
}
