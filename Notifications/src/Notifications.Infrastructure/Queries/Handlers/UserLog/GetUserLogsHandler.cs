﻿using MediatR;
using Notifications.Application.UseCases.UserLogs.Queries;

namespace Notifications.Infrastructure.Queries.Handlers.UserLog
{
    public sealed class GetUserLogsHandler : IRequestHandler<GetUserLogs,>
    {
    }
}
