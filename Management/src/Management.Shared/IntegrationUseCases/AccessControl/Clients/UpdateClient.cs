﻿using Management.Shared.IntegrationUseCases.AccessControl.DTOs;

namespace Management.Shared.IntegrationUseCases.AccessControl.Clients
{
    public sealed record UpdateClient(
        Guid Id,
        string FirstName,
        string SecondName,
        string? Patronymic,
        string? Phone,
        string Email,
        bool AllowEntry,
        MembershipDto? Membership
    );
}
