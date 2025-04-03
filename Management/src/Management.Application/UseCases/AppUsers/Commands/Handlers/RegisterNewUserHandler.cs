using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.Services;
using Management.Application.Services;
using Management.Domain.Entities;
using Management.Domain.Repositories;
using Management.Shared.DTOs;
using MediatR;

namespace Management.Application.UseCases.AppUsers.Commands.Handlers
{
    public sealed class RegisterNewUserHandler : IRequestHandler<RegisterNewUser, TokensDto?>
    {
        private readonly ITokenService _tokenService;
        private readonly IPasswordService _passwordService;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IRepository _repository;

        public RegisterNewUserHandler(
            ITokenService tokenService,
            IPasswordService passwordService, IRoleRepository roleRepository, IRepository repository,
            IUserRepository userRepository, IClientRepository clientRepository)
        {
            _tokenService = tokenService;
            _passwordService = passwordService;
            _roleRepository = roleRepository;
            _repository = repository;
            _userRepository = userRepository;
            _clientRepository = clientRepository;
        }

        public async Task<TokensDto?> Handle(RegisterNewUser command, CancellationToken cancellationToken)
        {
            var (firstName, secondName, patronymic, phone, email, password) = command;

            if (await _userRepository.ExistsByEmailAsync(email))
                throw new BadRequestException($"User with email {email} already exists");

            var role = await _roleRepository.GetByNameAsync(Role.Manager.Name)
                ?? throw new BadRequestException($"Cannot find role with name {Role.Manager.Name}");

            var passwordHash = _passwordService.HashPassword(password);

            var refreshToken = _tokenService.GenerateRefreshToken();
            var refreshTokenExpires = _tokenService.GenerateRefreshTokenExpiresDate();

            var user = AppUser.Create(firstName, secondName, patronymic, phone, email, passwordHash, false, true, refreshToken, refreshTokenExpires, role.Id);

            var accessToken = _tokenService.GenerateAccessToken(user.Id, firstName, secondName, patronymic, email, role.Name);

            await _userRepository.AddAsync(user);
            await _clientRepository.AddAsync(Client.Create(user.Id, firstName, secondName, patronymic, phone, email, true, false, null));

            await _repository.SaveChangesAsync();

            return new(accessToken, refreshToken);
        }
    }
}