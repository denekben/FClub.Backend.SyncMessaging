using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.Services;
using Management.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Management.Application.UseCases.AppUsers.Commands.Handlers
{
    public sealed class AssignUserToRoleHandler : IRequestHandler<AssignUserToRole>
    {
        private readonly ILogger<AssignUserToRoleHandler> _logger;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextService _contextService;
        private readonly IRepository _repository;

        public AssignUserToRoleHandler(
            ILogger<AssignUserToRoleHandler> logger, IRoleRepository roleRepository, IUserRepository userRepository, IHttpContextService contextService,
            IRepository repository)
        {
            _logger = logger;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _contextService = contextService;
            _repository = repository;
        }

        public async Task Handle(AssignUserToRole command, CancellationToken cancellationToken)
        {
            var currentUserId = _contextService.GetCurrentUserId()
                ?? throw new BadRequestException("Invalid authorization header");

            var (userId, roleId) = command;

            var user = await _userRepository.GetAsync(userId)
                ?? throw new NotFoundException($"Cannot find user {userId}");
            var currentUser = await _userRepository.GetAsync(currentUserId)
                ?? throw new NotFoundException($"Cannot find current user {currentUserId}");
            if (user.RoleId == currentUser.RoleId)
                throw new BadRequestException($"User cannot change role for user with same role");

            var role = await _roleRepository.GetAsync(roleId)
                ?? throw new NotFoundException($"Cannot find role {roleId}");

            user.RoleId = roleId;

            await _userRepository.UpdateAsync(user);
            await _repository.SaveChangesAsync();

            _logger.LogInformation($"User {userId} changed role to {roleId}");
        }
    }
}
