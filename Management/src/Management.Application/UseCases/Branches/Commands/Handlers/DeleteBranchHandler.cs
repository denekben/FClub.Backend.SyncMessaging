using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.Branches.Commands.Handlers
{
    public sealed class DeleteBranchHandler : IRequestHandler<DeleteBranch>
    {
        private readonly IBranchRepository _branchRepository;

        public DeleteBranchHandler(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task Handle(DeleteBranch command, CancellationToken cancellationToken)
        {
            await _branchRepository.DeleteAsync(command.Id);
        }
    }
}
