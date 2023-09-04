using Application.Features.ClientPositionsManager.Commands.CreateClientPositionManager;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers.v1.ClientPositions;

[ApiVersion("1.0")]
public class CreateClientPositionManagerController : BaseApiController
{
    public CreateClientPositionManagerController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public Task<IActionResult> CreateClientPositionManager(CreateClientPositionManagerCommand request,
        CancellationToken cancellationToken)
    {
        return ProcessCreateClientPositionManager(request, cancellationToken);
    }

    private async Task<IActionResult> ProcessCreateClientPositionManager(CreateClientPositionManagerCommand request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);
        return CreatedAtRoute("GetClientPositionManagerById", new { id = response }, request);
    }
}