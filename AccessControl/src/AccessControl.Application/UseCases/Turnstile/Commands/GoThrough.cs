using MediatR;

namespace AccessControl.Application.UseCases.Turnstile.Commands
{
    public sealed record GoThrough : IRequest;
}
