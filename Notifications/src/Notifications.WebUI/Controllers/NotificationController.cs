﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notifications.Application.UseCases.Notifications.Commands;
using Notifications.Application.UseCases.Notifications.Queries;
using Notifications.Domain.DTOs;

namespace Notifications.WebUI.Controllers
{
    [ApiController]
    [Authorize(Policy = "IsNotBlocked", Roles = "Manager,Admin")]
    [Route("api/notifications/notifications")]
    public class NotificationController : ControllerBase
    {
        private readonly ISender _sender;

        public NotificationController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult<NotificationDto?>> CreateNotification([FromBody] CreateNotification command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpDelete]
        [Route("{notificationId:guid}")]
        public async Task<ActionResult> DeleteNotification([FromRoute] Guid notificationId)
        {
            await _sender.Send(new DeleteNotification(notificationId));
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<NotificationDto?>> UpdateNotification([FromBody] UpdateNotification command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("send")]
        public async Task<ActionResult<NotificationDto?>> SendNotification([FromBody] SendNotification command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpPost]
        [Route("send-created")]
        public async Task<ActionResult<NotificationDto?>> SendCreatedNotification([FromBody] SendCreatedNotification command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<NotificationDto?>> GetNotifications([FromQuery] GetNotifications query)
        {
            var result = await _sender.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
