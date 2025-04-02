using MediatR;

namespace Management.Application.UseCases.AppUsers.Commands.Handlers
{
    public sealed class RefreshExpiredTokenHandler : IRequestHandler<RefreshExpiredToken>
    {
    }
}
