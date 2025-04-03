using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.Branches.Commands.Handlers
{
    public sealed class DeleteBranchHandler : IRequestHandler<DeleteBranch>
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IRepository _repository;

        public DeleteBranchHandler(IBranchRepository branchRepository, IRepository repository)
        {
            _branchRepository = branchRepository;
            _repository = repository;
        }

        public async Task Handle(DeleteBranch command, CancellationToken cancellationToken)
        {
            await _branchRepository.DeleteAsync(command.Id);
            await _repository.SaveChangesAsync();
        }
    }
}
