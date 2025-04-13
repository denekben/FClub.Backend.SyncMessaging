﻿using Management.Application.UseCases.Branches.Commands;
using Management.Application.UseCases.Branches.Queries;
using Management.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Management.WebUI.Controllers
{
    [ApiController]
    [Route("api/management/branches")]
    public class BranchController : ControllerBase
    {
        private readonly ISender _sender;

        public BranchController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult<BranchDto?>> CreateBranch([FromBody] CreateBranch command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpDelete]
        [Route("{branchId:guid}")]
        public async Task<ActionResult> DeleteBranch([FromRoute] DeleteBranch command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<BranchDto?>> UpdateBranch([FromBody] UpdateBranch command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Route("{branchId:guid}")]
        public async Task<ActionResult<List<BranchDto>?>> GetBranch([FromRoute] Guid branchId)
        {
            var result = await _sender.Send(new GetBranch(branchId));
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<BranchDto>?>> GetBranches([FromQuery] GetBranches query)
        {
            var result = await _sender.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
