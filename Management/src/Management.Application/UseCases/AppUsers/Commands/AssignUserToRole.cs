using MediatR;

namespace Management.Application.UseCases.AppUsers.Commands
{
    public sealed record AssignUserToRole(
        Guid UserId,
        Guid RoleId
    ) : IRequest;
}
