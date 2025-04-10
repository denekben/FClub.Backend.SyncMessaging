﻿using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.SocialGroups.Commands
{
    public sealed record CreateSocialGroup(string Name) : IRequest<SocialGroupDto?>;
}
