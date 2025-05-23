﻿using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.Tariffs.Commands
{
    public sealed record CreateTariff(
        bool SendNotification,
        string Name,
        Dictionary<int, int> PriceForNMonths,
        Dictionary<Guid, int>? DiscountForSocialGroup,
        bool AllowMultiBranches,
        List<string> ServiceNames
    ) : IRequest<TariffWithGroupsDto?>;
}