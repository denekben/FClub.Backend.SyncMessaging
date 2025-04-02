using Management.Shared.DTOs;
using MediatR;

namespace Management.Application.UseCases.AppUsers.Commands
{
    public sealed record SignIn(
        string Email,
        string Password
    ) : IRequest<TokensDto?>;
}
