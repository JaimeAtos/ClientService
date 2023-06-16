using Application.Features.LeaveRequests.Commands.CreateLeaveRequestCommand;
using Application.Features.LeaveRequests.Commands.DeleteLeaveRequestCommand;
using Application.Features.LeaveRequests.Commands.UpdateLeaveRquestCommand;
using Application.Features.LeaveRequests.Queries.GetAllLeaveRequestQuery;
using Application.Features.LeaveRequests.Queries.GetLeaveRequestByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers
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
        public async Task<IActionResult> DeleteLeaveRequest([FromBody] DeleteLeaveRequestCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpPut("/UpdateLeaveRequest")]
        public async Task<IActionResult> UpdateLeaveRequest([FromBody] UpdateLeaveRequestCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpGet("/GetAllLeaveRequests")]
        public async Task<IActionResult> GetAllLeaveRequests([FromQuery]GetAllLeaveRequestParameters filters)
        {
            return Ok(await _mediator.Send(new GetAllLeaveRequestQuery() 
            {
                PageNumber = filters.PageNumber, 
                PageSize = filters.PageSize,
                ResourceId = filters.ResourceId,
                LeaveReasonComments = filters.LeaveReasonComments,
                State = filters.State
            }));
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
