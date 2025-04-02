using Management.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Management.Application.UseCases.Branches.Commands.Handlers
{
    public sealed class DeleteBranchHandler : IRequestHandler<DeleteBranch>
    {
        private readonly ILogger<DeleteBranchHandler> _logger;
        private readonly IBranchRepository _branchRepository;

        public DeleteBranchHandler(ILogger<DeleteBranchHandler> logger, IBranchRepository branchRepository)
        {
            _logger = logger;
            _branchRepository = branchRepository;
        }

        public async Task Handle(DeleteBranch command, CancellationToken cancellationToken)
        {
            await _branchRepository.DeleteAsync(command.Id);

            _logger.LogInformation($"Branch {command.Id} deleted");
        }
    }
}
