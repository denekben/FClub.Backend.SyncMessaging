using FClub.Backend.Common.Exceptions;
using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.SocialGroups.Commands.Handlers
{
    public sealed class DeleteSocialGroupHandler : IRequestHandler<DeleteSocialGroup>
    {
        private readonly ISocialGroupRepository _socialGroupRepository;
        private readonly IRepository _repository;

        public DeleteSocialGroupHandler(ISocialGroupRepository socialGroupRepository, IRepository repository)
        {
            _socialGroupRepository = socialGroupRepository;
            _repository = repository;
        }

        public async Task Handle(DeleteSocialGroup command, CancellationToken cancellationToken)
        {
            var socialGroup = await _socialGroupRepository.GetAsync(command.Id)
                ?? throw new NotFoundException($"Cannot find social group {command.Id}");

            await _socialGroupRepository.DeleteAsync(command.Id);
            await _repository.SaveChangesAsync();
        }
    }
}
