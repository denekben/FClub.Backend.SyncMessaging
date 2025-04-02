using MediatR;

namespace Management.Application.UseCases.AppUsers.Commands
{
    public sealed record RefreshExpiredToken(string RefreshToken) : IRequest<string?>;
}
