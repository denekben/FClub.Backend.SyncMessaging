using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.Services;
using Management.Application.Services;
using Management.Domain.Repositories;
using Management.Shared.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Management.Application.UseCases.AppUsers.Commands.Handlers
{
    public sealed class SignInHandler : IRequestHandler<SignIn, TokensDto?>
    {
        private readonly ILogger<SignInHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        private readonly IRepository _repository;

        public SignInHandler(
            ILogger<SignInHandler> logger, IUserRepository userRepository, IPasswordService passwordService,
            ITokenService tokenService, IRepository repository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _passwordService = passwordService;
            _tokenService = tokenService;
            _repository = repository;
        }

        public async Task<TokensDto?> Handle(SignIn command, CancellationToken cancellationToken)
        {
            var (email, password) = command;

            var user = await _userRepository.GetUserByEmailAsync(email, UserIncludes.Role)
                ?? throw new BadRequestException("Invalid email or password");

            if (!_passwordService.IsPasswordValid(password, user.PasswordHash))
                throw new BadRequestException("Invalid email or password");

            var refreshToken = _tokenService.GenerateRefreshToken();
            var refreshTokenExpiresDate = _tokenService.GenerateRefreshTokenExpiresDate();
            var accessToken = _tokenService.GenerateAccessToken(user.Id, user.FullName.FirstName, user.FullName.SecondName, user.FullName.Patronymic, email, user.Role.Name);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpires = refreshTokenExpiresDate;

            await _userRepository.UpdateAsync(user);
            await _repository.SaveChangesAsync();
            _logger.LogInformation($"User {user.Id} signed in");

            return new(accessToken, refreshToken);
        }
    }
}
