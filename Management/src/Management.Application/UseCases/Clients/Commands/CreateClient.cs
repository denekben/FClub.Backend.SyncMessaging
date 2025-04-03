using Management.Shared.DTOs;
using MediatR;

namespace Management.Application.UseCases.Clients.Commands
{
    public sealed record CreateClient(
        string FirstName,
        string SecondName,
        string? Patronymic,
        string? Phone,
        string Email,
        bool AllowEntry,
        bool AllowNotifications,
        Guid? MembershipId
    ) : IRequest<ClientDto?>;
}
