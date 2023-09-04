using Application.Features.LeaveRequests.Commands.DeleteLeaveRequestCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers.v1.LeaveRequests;
[ApiVersion("1.0")]
public class DeleteLeaveRequestController: BaseApiController
{
    public DeleteLeaveRequestController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpDelete]
    public Task<IActionResult> DeleteLeaveRequest(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        return ProcessDeleteLeaveRequest(request, cancellationToken);
    }

    private async Task<IActionResult> ProcessDeleteLeaveRequest(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }
}