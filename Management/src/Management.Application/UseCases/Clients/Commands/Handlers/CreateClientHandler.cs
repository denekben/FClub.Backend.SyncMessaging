using Management.Domain.Entities;
using Management.Domain.Repositories;
using Management.Shared.DTOs;
using MediatR;

namespace Management.Application.UseCases.Clients.Commands.Handlers
{
    public sealed class CreateClientHandler : IRequestHandler<CreateClient, ClientDto?>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IRepository _repository;

        public CreateClientHandler(IClientRepository clientRepository, IRepository repository)
        {
            _clientRepository = clientRepository;
            _repository = repository;
        }

        public async Task<ClientDto?> Handle(CreateClient command, CancellationToken cancellationToken)
        {
            var (firstName, secondName, patronymic, phone, email, allowEntry, allowNotifications, membershipId) = command;

            var client = Client.Create(firstName, secondName, patronymic, phone, email, allowEntry, allowNotifications, membershipId);

            await _clientRepository.AddAsync(client);
            await _repository.SaveChangesAsync();

            return client.AsDto();
        }
    }
}
