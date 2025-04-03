using MediatR;

namespace Management.Application.UseCases.SocialGroups.Commands
{
    public sealed record UpdateSocialGroup(Guid Id, string Name) : IRequest<SocialGroupDto?>;
}
