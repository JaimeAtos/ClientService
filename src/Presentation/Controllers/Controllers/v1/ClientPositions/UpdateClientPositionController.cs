using Application.Features.ClientPositions.Commands.UpdateClientPositionCommnad;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers.v1.ClientPositions;

[ApiVersion("1.0")]
public class UpdateClientPositionController : BaseApiController
{
    public UpdateClientPositionController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPut]
    public Task<IActionResult> UpdateClientPosition(UpdateClientPositionCommand request,
        CancellationToken cancellationToken)
    {
        return ProcessUpdateClientPosition(request, cancellationToken);
    }

    private async Task<IActionResult> ProcessUpdateClientPosition(UpdateClientPositionCommand request,
        CancellationToken cancellationToken)
    {
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }
}