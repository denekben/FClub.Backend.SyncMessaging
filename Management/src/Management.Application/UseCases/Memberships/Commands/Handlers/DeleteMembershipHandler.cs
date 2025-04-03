using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.Memberships.Commands.Handlers
{
    public sealed class DeleteMembershipHandler : IRequestHandler<DeleteMembership>
    {
        private readonly IRepository _repository;
        private readonly IMembershipRepository _membershipRepository;

        public DeleteMembershipHandler(IRepository repository, IMembershipRepository membershipRepository)
        {
            _repository = repository;
            _membershipRepository = membershipRepository;
        }

        public async Task Handle(DeleteMembership command, CancellationToken cancellationToken)
        {
            await _membershipRepository.DeleteAsync(command.ClientId);
            await _repository.SaveChangesAsync();
        }
    }
}
