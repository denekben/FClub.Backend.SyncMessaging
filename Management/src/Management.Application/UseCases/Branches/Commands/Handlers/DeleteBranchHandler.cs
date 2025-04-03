using FClub.Backend.Common.Exceptions;
using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.Branches.Commands.Handlers
{
    public sealed class DeleteBranchHandler : IRequestHandler<DeleteBranch>
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IRepository _repository;

        public DeleteBranchHandler(
            IBranchRepository branchRepository, IRepository repository,
            IServiceRepository serviceRepository)
        {
            _branchRepository = branchRepository;
            _repository = repository;
            _serviceRepository = serviceRepository;
        }

        public async Task Handle(DeleteBranch command, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.GetAsync(command.Id, BranchIncludes.ServiceBranches)
                ?? throw new BadRequestException($"Cannot find branch {command.Id}");

            var serviceIds = branch.ServiceBranches.Select(sb => sb.ServiceId).ToList();

            await _serviceRepository.DeleteOneBranchServicesAsync(serviceIds);

            await _branchRepository.DeleteAsync(branch.Id);
            await _repository.SaveChangesAsync();
        }
    }
}
