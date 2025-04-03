using Management.Domain.Entities;
using Management.Domain.Repositories;
using Management.Shared.DTOs;
using MediatR;

namespace Management.Application.UseCases.SocialGroups.Commands.Handlers
{
    public sealed class CreateSocialGroupHandler : IRequestHandler<CreateSocialGroup, SocialGroupDto?>
    {
        private readonly ISocialGroupRepository _socialGroupRepository;
        private readonly IRepository _repository;

        public CreateSocialGroupHandler(ISocialGroupRepository socialGroupRepository, IRepository repository)
        {
            _socialGroupRepository = socialGroupRepository;
            _repository = repository;
        }

        public async Task<SocialGroupDto?> Handle(CreateSocialGroup command, CancellationToken cancellationToken)
        {
            var socialGroup = SocialGroup.Create(command.Name);

            await _socialGroupRepository.AddAsync(socialGroup);
            await _repository.SaveChangesAsync();

            return socialGroup.AsDto();
        }
    }
}
