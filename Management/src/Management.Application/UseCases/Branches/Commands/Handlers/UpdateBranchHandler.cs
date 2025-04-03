using FClub.Backend.Common.Exceptions;
using Management.Domain.Entities;
using Management.Domain.Entities.Pivots;
using Management.Domain.Repositories;
using Management.Shared.DTOs;
using MediatR;

namespace Management.Application.UseCases.Branches.Commands.Handlers
{
    public sealed class UpdateBranchHandler : IRequestHandler<UpdateBranch, BranchDto?>
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IRepository _repository;

        public UpdateBranchHandler(
            IBranchRepository branchRepository, IServiceRepository serviceRepository, IRepository repository
        )
        {
            _branchRepository = branchRepository;
            _serviceRepository = serviceRepository;
            _repository = repository;
        }

        public async Task<BranchDto?> Handle(UpdateBranch command, CancellationToken cancellationToken)
        {
            var (branchId, name, country, city, street, houseNumber, serviceNames) = command;

            var branch = await _branchRepository.GetAsync(branchId, BranchIncludes.ServiceBranches | BranchIncludes.Services)
                ?? throw new NotFoundException($"Cannot find {branchId}");

            branch.UpdateDetails(name, country, city, street, houseNumber);

            var serviceNamesToDelete = branch.ServiceBranches.Select(sb => sb.Service.Name).Except(serviceNames).ToList();
            var serviceNamesToAdd = serviceNames.Except(branch.ServiceBranches.Select(sb => sb.Service.Name)).ToList();

            foreach (var serviceName in serviceNamesToAdd)
            {
                var service = await _serviceRepository.GetByNameAsync(serviceName);
                if (service == null)
                {
                    service = Service.Create(serviceName);
                    await _serviceRepository.AddAsync(service);
                }
                branch.ServiceBranches.Add(ServiceBranch.Create(service.Id, branch.Id));
            }

            await _serviceRepository.DeleteOneBranchServicesByNameAsync(serviceNamesToDelete, branch.Id);
            foreach (var serviceName in serviceNamesToDelete)
            {
                var service = await _serviceRepository.GetByNameAsync(serviceName);
                if (service != null)
                {
                    var serviceBranchToRemove = branch.ServiceBranches.FirstOrDefault(sb => sb.ServiceId == service.Id);
                    if (serviceBranchToRemove != null)
                    {
                        branch.ServiceBranches.Remove(serviceBranchToRemove);
                    }
                }
            }

            await _branchRepository.UpdateAsync(branch);
            await _repository.SaveChangesAsync();

            return branch.AsDto();
        }
    }
}
