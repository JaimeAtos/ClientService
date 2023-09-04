using Application.Features.Clients.Commands.UpdateClientCommnad;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers.v1.LeaveRequests;
[ApiVersion("1.0")]
public class UpdateLeaveRequestController : BaseApiController
{
    public UpdateLeaveRequestController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPut]
    public Task<IActionResult> UpdateClient(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        return ProcessUpdateClient(request, cancellationToken);
    }

    private async Task<IActionResult> ProcessUpdateClient(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }
}