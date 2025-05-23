﻿using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Notifications.Domain.Repositories;

namespace Notifications.WebUI.Policies.IsNotBlocked
{
    public class IsNotBlockedHandler : AuthorizationHandler<IsNotBlockedRequirement>
    {
        private readonly IHttpContextService _httpContextService;
        private readonly IUserRepository _userRepository;

        public IsNotBlockedHandler(IHttpContextService httpContextService, IUserRepository userRepository)
        {
            _httpContextService = httpContextService;
            _userRepository = userRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IsNotBlockedRequirement requirement)
        {
            var userId = _httpContextService.GetCurrentUserId()
                ?? throw new BadRequestException("Invalid authorization header");
            var result = await _userRepository.IsBlocked(userId);
            if (result != null && !(bool)result)
            {
                context.Succeed(requirement);
            }
        }
    }
}
