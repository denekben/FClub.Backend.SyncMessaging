﻿using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.Logging;
using FClub.Backend.Common.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.UseCases.UserLogs.Queries;
using Notifications.Domain.DTOs;
using Notifications.Domain.DTOs.Mappers;
using Notifications.Infrastructure.Data;

namespace Notifications.Infrastructure.Queries.Handlers.UserLog
{
    [SkipLogging]
    public sealed class GetCurrentUserLogsHandler : IRequestHandler<GetCurrentUserLogs, List<UserLogDto>?>
    {
        private readonly AppLogDbContext _context;
        private readonly IHttpContextService _contextService;

        public GetCurrentUserLogsHandler(AppLogDbContext context, IHttpContextService contextService)
        {
            _context = context;
            _contextService = contextService;
        }

        public async Task<List<UserLogDto>?> Handle(GetCurrentUserLogs query, CancellationToken cancellationToken)
        {
            var (textSearchPhrase, sortByCreatedDate, pageNumber, pageSize) = query;

            var userId = _contextService.GetCurrentUserId()
                ?? throw new BadRequestException("Invalid authorization header");

            var logs = _context.UserLogs.Where(l => l.AppUserId == userId).AsQueryable();

            if (!string.IsNullOrWhiteSpace(textSearchPhrase))
                logs = logs.Where(l => EF.Functions.ILike(l.Text, $"%{textSearchPhrase.Trim()}%"));

            logs = sortByCreatedDate switch
            {
                true => logs.OrderBy(l => l.CreatedDate),
                false => logs.OrderByDescending(l => l.CreatedDate),
                _ => logs
            };

            int skipNumber = (pageNumber - 1) * pageSize;

            logs = logs.Skip(skipNumber).Take(pageSize);

            return await logs.Select(l => l.AsDto()).ToListAsync();
        }
    }
}