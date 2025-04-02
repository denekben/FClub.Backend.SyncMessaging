using Management.Shared.DTOs;
using MediatR;

namespace Management.Application.UseCases.AppUsers.Commands
{
    public sealed record RegisterNewUser(
        string FirstName,
        string SecondName,
        string? Patronymic,
        string? Phone,
        string Email,
        string Password
    ) : IRequest<TokensDto?>;
}