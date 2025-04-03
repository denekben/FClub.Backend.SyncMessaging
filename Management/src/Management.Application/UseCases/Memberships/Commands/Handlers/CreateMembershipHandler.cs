using FClub.Backend.Common.Exceptions;
using Management.Domain.Entities;
using Management.Domain.Repositories;
using Management.Shared.DTOs;
using MediatR;

namespace Management.Application.UseCases.Memberships.Commands.Handlers
{
    public sealed class CreateMembershipHandler : IRequestHandler<CreateMembership, MembershipDto?>
    {
        private IRepository _repository;
        private IMembershipRepository _membershipRepository;
        private IClientRepository _clientRepository;

        public CreateMembershipHandler(
            IRepository repository, IMembershipRepository membershipRepository,
            IClientRepository clientRepository)
        {
            _repository = repository;
            _membershipRepository = membershipRepository;
            _clientRepository = clientRepository;
        }

        public async Task<MembershipDto?> Handle(CreateMembership command, CancellationToken cancellationToken)
        {
            var (tariffId, expiresDate, clientId) = command;

            var client = await _clientRepository.GetAsync(clientId)
                ?? throw new NotFoundException($"Cannot find client {clientId}");

            if (client.MembershipId != null)
                throw new BadRequestException($"Client already have membership");

            var membership = Membership.Create(tariffId, expiresDate, clientId);

            await _membershipRepository.AddAsync(membership);
            await _repository.SaveChangesAsync();

            return membership.AsDto();
        }
    }
}
