using Application.Features.LeaveRequests.Commands.CreateLeaveRequestCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers.v1.LeaveRequests;
[ApiVersion("1.0")]
public class CreateLeaveRequestController : BaseApiController
{
    public CreateLeaveRequestController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpPost]
    public Task<IActionResult> CreateLeaveRequest(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        return ProcessCreateLeaveRequest(request, cancellationToken);
    }

    private async Task<IActionResult> ProcessCreateLeaveRequest(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);
        return CreatedAtRoute("GetLeaveRequestById", new { id = response }, request);
    }
}