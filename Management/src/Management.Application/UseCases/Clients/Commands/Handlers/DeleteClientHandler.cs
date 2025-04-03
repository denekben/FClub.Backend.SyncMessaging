using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.Services;
using Management.Domain.Entities;
using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.Clients.Commands.Handlers
{
    public sealed class DeleteClientHandler : IRequestHandler<DeleteClient>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository _repository;
        private readonly IHttpContextService _contextService;

        public DeleteClientHandler(
            IClientRepository clientRepository,
            IRepository repository, IUserRepository userRepository, IHttpContextService contextService)
        {
            _clientRepository = clientRepository;
            _repository = repository;
            _userRepository = userRepository;
            _contextService = contextService;
        }

        public async Task Handle(DeleteClient command, CancellationToken cancellationToken)
        {
            var userId = _contextService.GetCurrentUserId()
                ?? throw new BadRequestException("Invalid authorization header");

            var currentUser = await _userRepository.GetAsync(userId, UserIncludes.Role)
                ?? throw new BadRequestException("Invalid authorization header");
            var deletingUser = await _userRepository.GetAsync(command.Id, UserIncludes.Role);

            if (currentUser.Role.Name != Role.Admin.Name && deletingUser?.Role.Name == Role.Admin.Name)
                throw new BadRequestException("Only admin can delete admin client");

            await _clientRepository.DeleteAsync(command.Id);
            await _repository.SaveChangesAsync();
        }
    }
}
