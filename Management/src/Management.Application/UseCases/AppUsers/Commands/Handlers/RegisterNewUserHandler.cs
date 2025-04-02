using Management.Shared.DTOs;
using MediatR;

namespace Management.Application.UseCases.AppUsers.Commands.Handlers
{
    public sealed class RegisterNewUserHandler : IRequestHandler<RegisterNewUser, TokensDto?>
    {
        private readonly ILogger

        public RegisterNewUserHandler()
        {

        }

        public Task<TokensDto?> Handle(RegisterNewUser request, CancellationToken cancellationToken)
        {

        }
    }
}
