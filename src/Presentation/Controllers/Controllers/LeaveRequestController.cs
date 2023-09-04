using Application.Features.LeaveRequests.Commands.CreateLeaveRequestCommand;
using Application.Features.LeaveRequests.Commands.DeleteLeaveRequestCommand;
using Application.Features.LeaveRequests.Commands.UpdateLeaveRquestCommand;
using Application.Features.LeaveRequests.Queries.GetAllLeaveRequestQuery;
using Application.Features.LeaveRequests.Queries.GetLeaveRequestByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers
{
    [ApiVersion("1.0")]
    public class LeaveRequestController : BaseApiController
    {
        public LeaveRequestController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateLeaveRequest([FromBody] CreateLeaveRequestCommand command)
        {
            var response = await Mediator.Send(command);
            return CreatedAtRoute("GetLeaveRequestById", new { id = response.Data }, command);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLeaveRequest([FromBody] DeleteLeaveRequestCommand command,
            CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLeaveRequest([FromBody] UpdateLeaveRequestCommand command,
            CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLeaveRequests([FromQuery] GetAllLeaveRequestParameters filters)
        {
            return Ok(await Mediator.Send(new GetAllLeaveRequestQuery()
            {
                PageNumber = filters.PageNumber,
                PageSize = filters.PageSize,
                ResourceId = filters.ResourceId,
                LeaveReasonComments = filters.LeaveReasonComments,
                State = filters.State
            }));
        }

        [HttpGet("{id}", Name = "GetLeaveRequestById")]
        public async Task<IActionResult> GetLeaveRequestById(Guid id)
        {
            var query = new GetLeaveRequestByIdQuery() { Id = id };
            var response = await Mediator.Send(query);
            return Ok(response);
        }
    }
}