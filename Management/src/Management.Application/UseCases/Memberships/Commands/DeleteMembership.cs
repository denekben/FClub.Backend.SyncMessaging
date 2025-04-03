using MediatR;

namespace Management.Application.UseCases.Memberships.Commands
{
    public sealed record DeleteMembership(
        Guid ClientId
    ) : IRequest;
}
