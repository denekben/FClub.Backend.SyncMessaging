using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.Services;
using Management.Domain.Entities;
using Management.Domain.Repositories;
using Management.Shared.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Management.Application.UseCases.Clients.Commands.Handlers
{
    public sealed class UpdateClientHandler : IRequestHandler<UpdateClient, ClientDto?>
    {
        private readonly ILogger<UpdateClientHandler> _logger;
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextService _contextService;
        private readonly IRepository _repository;

        public UpdateClientHandler(
            ILogger<UpdateClientHandler> logger, IClientRepository clientRepository,
            IUserRepository userRepository, IRepository repository, IHttpContextService contextService)
        {
            _logger = logger;
            _clientRepository = clientRepository;
            _userRepository = userRepository;
            _repository = repository;
            _contextService = contextService;
        }

        public async Task<ClientDto?> Handle(UpdateClient command, CancellationToken cancellationToken)
        {
            var (id, firstName, secondName, patronymic, phone, email, allowEntry, allowNotifications, membershipId) = command;

            var userId = _contextService.GetCurrentUserId()
                ?? throw new BadRequestException("Invalid authorization header");

            var currentUser = await _userRepository.GetAsync(userId, UserIncludes.Role)
                ?? throw new BadRequestException("Invalid authorization header");
            var updatingUser = await _userRepository.GetAsync(id, UserIncludes.Role);

            if (currentUser.Role.Name != Role.Admin.Name && updatingUser?.Role.Name == Role.Admin.Name)
                throw new BadRequestException("Only admin can update admin client");

            var updatingClient = await _clientRepository.GetAsync(id)
                ?? throw new NotFoundException($"Cannot find client {id}");

            updatingClient.UpdateDetails(id, firstName, secondName, patronymic, phone, email, allowEntry, allowNotifications, membershipId);

            await _clientRepository.UpdateAsync(updatingClient);
            await _repository.SaveChangesAsync();

            _logger.LogInformation($"Client {updatingClient.Id} updated");

            return updatingClient.AsDto();
        }
    }
}