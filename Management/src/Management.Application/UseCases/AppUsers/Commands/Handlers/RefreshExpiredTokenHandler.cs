using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.Services;
using Management.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Management.Application.UseCases.AppUsers.Commands.Handlers
{
    public sealed class RefreshExpiredTokenHandler : IRequestHandler<RefreshExpiredToken, string?>
    {
        private readonly ILogger<RefreshExpiredTokenHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextService _contextService;
        private readonly IRoleRepository _roleRepository;

        public RefreshExpiredTokenHandler(
            ILogger<RefreshExpiredTokenHandler> logger, IUserRepository userRepository, ITokenService tokenService,
            IHttpContextService contextService, IRoleRepository roleRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _contextService = contextService;
            _roleRepository = roleRepository;
        }

        public async Task<string?> Handle(RefreshExpiredToken command, CancellationToken cancellationToken)
        {
            var userId = _contextService.GetCurrentUserId()
                ?? throw new BadRequestException("Invalid authorization header");

            var user = await _userRepository.GetAsync(userId)
                ?? throw new NotFoundException($"Cannot find user {userId}");

            if (user.RefreshTokenExpires < DateTime.UtcNow)
                throw new BadRequestException("Refresh token expired");
            if (user.RefreshToken != command.RefreshToken)
                throw new BadRequestException("Incorrect refresh token");

            var role = await _roleRepository.GetAsync(user.RoleId)
                ?? throw new NotFoundException($"Cannot find role {user.RoleId}");

            var token = _tokenService.GenerateAccessToken(user.Id, user.FullName.FirstName, user.FullName.SecondName, user.FullName.Patronymic, user.Email, role.Name);

            _logger.LogInformation($"User {user.Id} refreshed token");
            return token;
        }
    }
}
