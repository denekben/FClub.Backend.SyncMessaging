using MediatR;

namespace Management.Application.UseCases.SocialGroups.Commands
{
    public sealed record DeleteSocialGroup(Guid Id) : IRequest;
}
