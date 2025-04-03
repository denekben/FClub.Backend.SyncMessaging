using FClub.Backend.Common.Exceptions;
using Management.Domain.Entities.Pivots;
using Management.Domain.Repositories;
using Management.Shared.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Management.Application.UseCases.Branches.Commands.Handlers
{
    public sealed class UpdateBranchHandler : IRequestHandler<UpdateBranch, BranchDto?>
    {
        private readonly ILogger<UpdateBranchHandler> _logger;
        private readonly IBranchRepository _branchRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IRepository _repository;

        public UpdateBranchHandler(
            ILogger<UpdateBranchHandler> logger, IBranchRepository branchRepository, IServiceRepository serviceRepository,
            IRepository repository
        )
        {
            _logger = logger;
            _branchRepository = branchRepository;
            _serviceRepository = serviceRepository;
            _repository = repository;
        }

        public async Task<BranchDto?> Handle(UpdateBranch command, CancellationToken cancellationToken)
        {
            var (branchId, name, country, city, street, houseNumber, servicesIds) = command;

            var branch = await _branchRepository.GetAsync(branchId, BranchIncludes.ServiceBranches)
                ?? throw new NotFoundException($"Cannot find {branchId}");

            branch.UpdateDetails(name, country, city, street, houseNumber);

            if (!await _serviceRepository.AllExists(servicesIds))
                throw new NotFoundException($"Cannot find services");

            var servicesIdsToDelete = branch.ServiceBranches.Select(sb => sb.ServiceId).Except(servicesIds).ToList();
            var servicesIdsToAdd = servicesIds.Except(branch.ServiceBranches.Select(sb => sb.ServiceId)).ToList();

            foreach (var serviceId in servicesIdsToAdd)
            {
                branch.ServiceBranches.Add(ServiceBranch.Create(serviceId, branch.Id));
            }

            foreach (var serviceId in servicesIdsToDelete)
            {
                branch.ServiceBranches.Remove(branch.ServiceBranches.First(sb => sb.ServiceId == serviceId));
            }

            await _branchRepository.UpdateAsync(branch);
            await _repository.SaveChangesAsync();
            _logger.LogInformation($"Branch {branch.Id} updated");

            return branch.AsDto();
        }
    }
}
