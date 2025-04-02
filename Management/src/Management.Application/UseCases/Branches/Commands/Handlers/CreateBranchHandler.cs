using FClub.Backend.Common.Exceptions;
using Management.Domain.Entities;
using Management.Domain.Entities.Pivots;
using Management.Domain.Repositories;
using Management.Shared.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Management.Application.UseCases.Branches.Commands.Handlers
{
    public sealed class CreateBranchHandler : IRequestHandler<CreateBranch, BranchDto?>
    {
        private readonly ILogger<CreateBranchHandler> _logger;
        private readonly IServiceRepository _serviceRepository;
        private readonly IBranchRepository _branchRepository;

        public CreateBranchHandler(
            ILogger<CreateBranchHandler> logger, IServiceRepository serviceRepository, IBranchRepository branchRepository)
        {
            _logger = logger;
            _serviceRepository = serviceRepository;
            _branchRepository = branchRepository;
        }

        public async Task<BranchDto?> Handle(CreateBranch command, CancellationToken cancellationToken)
        {
            var (name, country, city, street, houseNumber, servicesIds) = command;

            var branch = Branch.Create(name, country, city, street, houseNumber);

            foreach (var serviceId in servicesIds)
            {
                if (await _serviceRepository.GetAsync(serviceId) == null)
                    throw new NotFoundException($"Cannot find service {serviceId}");

                branch.ServiceBranches.Add(ServiceBranch.Create(serviceId, branch.Id));
            }

            await _branchRepository.AddAsync(branch);
            await _branchRepository.SaveChangesAsync();
            _logger.LogInformation($"Branch {branch.Id} created");

            return branch.AsDto();
        }
    }
}