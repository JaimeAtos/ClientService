using Application.Features.ClientPositionsManager.Commands.UpdateClientPositionManagerCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers.v1.ClientPositionManagers;

[ApiVersion("1.0")]
public class UpdateClientPositionManagerController : BaseApiController
{
    public UpdateClientPositionManagerController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPut]
    public Task<IActionResult> UpdateClientPositionManager(UpdateClientPositionManagerCommand request,
        CancellationToken cancellationToken)
    {
        return ProcessUpdateClientPositionManager(request, cancellationToken);
    }

    private async Task<IActionResult> ProcessUpdateClientPositionManager(UpdateClientPositionManagerCommand request,
        CancellationToken cancellationToken)
    {
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }
}