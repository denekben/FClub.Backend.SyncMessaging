using MediatR;

namespace Management.Application.UseCases.AppUsers.Commands
{
    public sealed record RefreshExpiredToken : IRequest;
}
