using Management.Application.UseCases.AppUsers.Commands;
using Management.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Management.WebUI.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        public async Task<IActionResult> AssignUserToRole(AssignUserToRole command)
        {
            await _sender.Send(command);

            return NoContent();
        }

        public async Task<ActionResult<string?>> RefreshExpiredToken(RefreshExpiredToken command)
        {
            return Ok(await _sender.Send(command));
        }

        public async Task<ActionResult<TokensDto?>> RegisterNewUser(RegisterNewUser command)
        {
            return Ok(await _sender.Send(command));
        }

        public async Task<ActionResult<TokensDto?>> SignIn(SignIn command)
        {
            return Ok(await _sender.Send(command));
        }
    }
}
