using Management.Shared.DTOs;
using MediatR;

namespace Management.Application.UseCases.Branches.Commands
{
    public sealed record CreateBranch(
        string? Name,
        string? Country,
        string? City,
        string? Street,
        string? HouseNumber,
        List<Guid> servicesIds
    ) : IRequest<BranchDto?>;
}