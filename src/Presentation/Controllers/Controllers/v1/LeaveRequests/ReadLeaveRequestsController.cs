using Application.Features.LeaveRequests.Queries.GetAllLeaveRequestQuery;
using Application.Features.LeaveRequests.Queries.GetLeaveRequestByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers.v1.LeaveRequests;
[ApiVersion("1.0")]
public class ReadLeaveRequestsController : BaseApiController
{
    public ReadLeaveRequestsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{id}", Name = "GetLeaveRequestById")]
    public async Task<IActionResult> GetLeaveRequestById(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetLeaveRequestByIdQuery { Id = id }, cancellationToken));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCollaborators([FromQuery] GetAllLeaveRequestParameters filters,
        CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAllLeaveRequestQuery
        {
            PageNumber = filters.PageNumber,
            PageSize = filters.PageSize,
            ResourceId = filters.ResourceId,
            LeaveReasonComments = filters.LeaveReasonComments,
            State = filters.State
        }, cancellationToken));
    }
}