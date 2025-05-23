﻿namespace Management.Shared.IntegrationUseCases.AccessControl.Clients
{
    public sealed record CreateClient(
        Guid Id,
        string FirstName,
        string SecondName,
        string? Patronymic,
        string? Phone,
        string Email,
        bool AllowEntry
    );
}
