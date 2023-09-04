using Application.Features.ClientPositions.Commands.DeleteClientPositionCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers.v1.ClientPositions;
[ApiVersion("1.0")]
public class DeleteClientPositionController: BaseApiController
{
    public DeleteClientPositionController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpDelete]
    public Task<IActionResult> DeleteClientPosition(DeleteClientPositionCommand request, CancellationToken cancellationToken)
    {
        return ProcessDeleteClientPosition(request, cancellationToken);
    }

    private async Task<IActionResult> ProcessDeleteClientPosition(DeleteClientPositionCommand request, CancellationToken cancellationToken)
    {
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }
}