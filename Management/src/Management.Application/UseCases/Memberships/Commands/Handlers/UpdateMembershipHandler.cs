using FClub.Backend.Common.Exceptions;
using Management.Domain.Repositories;
using Management.Shared.DTOs;
using MediatR;

namespace Management.Application.UseCases.Memberships.Commands.Handlers
{
    public sealed class UpdateMembershipHandler : IRequestHandler<UpdateMembership, MembershipDto?>
    {
        private readonly IRepository _repository;
        private readonly IMembershipRepository _membershipRepository;

        public UpdateMembershipHandler(IRepository repository, IMembershipRepository membershipRepository)
        {
            _repository = repository;
            _membershipRepository = membershipRepository;
        }

        public async Task<MembershipDto?> Handle(UpdateMembership command, CancellationToken cancellationToken)
        {
            var (membershipId, tariffId, expiresDate, clientId) = command;

            var membership = await _membershipRepository.GetAsync(membershipId)
                ?? throw new NotFoundException($"Cannot find membership {membershipId}");

            membership.UpdateDetails(membershipId, tariffId, expiresDate, clientId);

            await _membershipRepository.UpdateAsync(membership);
            await _repository.SaveChangesAsync();

            return membership.AsDto();
        }
    }
}
