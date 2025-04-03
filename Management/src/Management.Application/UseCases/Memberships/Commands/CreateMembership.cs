using Management.Shared.DTOs;
using MediatR;

namespace Management.Application.UseCases.Memberships.Commands
{
    public sealed record CreateMembership(
        Guid TariffId,
        DateTime ExpiresDate,
        Guid ClientId
    ) : IRequest<MembershipDto?>;
}