using FClub.Backend.Common.Exceptions;
using Management.Domain.Entities;
using Management.Domain.Entities.Pivots;
using Management.Domain.Repositories;
using Management.Shared.DTOs;
using MediatR;

namespace Management.Application.UseCases.Branches.Commands.Handlers
{
    public sealed class CreateBranchHandler : IRequestHandler<CreateBranch, BranchDto?>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IRepository _repository;

        public CreateBranchHandler(
            IServiceRepository serviceRepository, IBranchRepository branchRepository, IRepository repository)
        {
            _serviceRepository = serviceRepository;
            _branchRepository = branchRepository;
            _repository = repository;
        }

        public async Task<BranchDto?> Handle(CreateBranch command, CancellationToken cancellationToken)
        {
            var (name, country, city, street, houseNumber, servicesIds) = command;

            var branch = Branch.Create(name, country, city, street, houseNumber);

            if (!await _serviceRepository.AllExists(servicesIds))
                throw new NotFoundException($"Cannot find services");

            foreach (var serviceId in servicesIds)
            {
                branch.ServiceBranches.Add(ServiceBranch.Create(serviceId, branch.Id));
            }

            await _branchRepository.AddAsync(branch);
            await _repository.SaveChangesAsync();

            return branch.AsDto();
        }
    }
}