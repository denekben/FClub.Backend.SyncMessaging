﻿using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Clients
{
    public sealed record UpdateClient(
        Guid Id,
        string FirstName,
        string SecondName,
        string? Patronymic,
        string? Phone,
        string Email,
        bool AllowEntry
    ) : IRequest;
}
