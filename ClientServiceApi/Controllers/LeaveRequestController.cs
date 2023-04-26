using Application.Features.LeaveRequest.Commands.CreateLeaveRequestCommand;
using Application.Features.LeaveRequest.Commands.DeleteLeaveRequestCommand;
using Application.Features.LeaveRequest.Commands.UpdateLeaveRquestCommand;
using Application.Features.LeaveRequest.Queries.GetAllLeaveRequestQuery;
using Application.Features.LeaveRequest.Queries.GetLeaveRequestByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClientServiceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/CreateLeaveRequest")]
        public async Task<IActionResult> CreateLeaveRequest([FromBody] CreateLeaveRequestCommand command)
        {
            var response = await _mediator.Send(command);
            return CreatedAtRoute("GetLeaveRequestById", new {id = response.Data}, command);
        }

        [HttpDelete("/DeleteLeaveRequest")]
        public async Task<IActionResult> DeleteLeaveRequest([FromBody] DeleteLeaveRequestCommand command)
        {
            var response = await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("/UpdateLeaveRequest")]
        public async Task<IActionResult> UpdateLeaveRequest([FromBody] UpdateLeaveRquestCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("/GetAllLeaveRequests")]
        public async Task<IActionResult> GetAllLeaveRequests()
        {
            var query = new GetAllLeaveRequestQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("/GetLeaveRequestById/{id}", Name = "GetLeaveRequestById")]
        public async Task<IActionResult> GetLeaveRequestById(Guid id)
        {
            var query = new GetLeaveRequestByIdQuery() { Id = id };
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
