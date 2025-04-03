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

        public RefreshExpiredTokenHandler(
            ILogger<RefreshExpiredTokenHandler> logger, IUserRepository userRepository, ITokenService tokenService,
            IHttpContextService contextService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _contextService = contextService;
        }

        public async Task<string?> Handle(RefreshExpiredToken command, CancellationToken cancellationToken)
        {
            var userId = _contextService.GetCurrentUserId()
                ?? throw new BadRequestException("Invalid authorization header");

            var user = await _userRepository.GetAsync(userId, UserIncludes.Role)
                ?? throw new NotFoundException($"Cannot find user {userId}");

            if (user.RefreshTokenExpires < DateTime.UtcNow)
                throw new BadRequestException("Refresh token expired");
            if (user.RefreshToken != command.RefreshToken)
                throw new BadRequestException("Incorrect refresh token");

            var token = _tokenService.GenerateAccessToken(user.Id, user.FullName.FirstName, user.FullName.SecondName, user.FullName.Patronymic, user.Email, user.Role.Name);

            _logger.LogInformation($"User {user.Id} refreshed token");
            return token;
        }
    }
}
