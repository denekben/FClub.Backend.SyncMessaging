﻿using AccessControl.Domain.DTOs;
using MediatR;

namespace AccessControl.Application.UseCases.UserLogs.Queries
{
    public sealed record GetUserLogsByUserId : IRequest<List<UserLogDto>?>;
}
