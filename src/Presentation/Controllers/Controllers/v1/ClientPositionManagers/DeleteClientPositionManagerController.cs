using Application.Features.ClientPositionsManager.Commands.DeleteClientPositionManagerCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers.v1.ClientPositionManagers;

[ApiVersion("1.0")]
public class DeleteClientPositionManagerController : BaseApiController
{
    public DeleteClientPositionManagerController(IMediator mediator) : base(mediator)
    {
    }

    [HttpDelete]
    public Task<IActionResult> DeleteClientPositionManager(DeleteClientPositionManagerCommand request,
        CancellationToken cancellationToken)
    {
        return ProcessDeleteClientPositionManager(request, cancellationToken);
    }

    private async Task<IActionResult> ProcessDeleteClientPositionManager(DeleteClientPositionManagerCommand request,
        CancellationToken cancellationToken)
    {
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }
}