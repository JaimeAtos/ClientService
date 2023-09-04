using Application.Features.ClientPositions.Commands.CreateClientPositionCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers.v1.ClientPositionManagers;

[ApiVersion("1.0")]
public class CreateClientPositionController : BaseApiController
{
    public CreateClientPositionController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public Task<IActionResult> CreateClientPosition(CreateClientPositionCommand request,
        CancellationToken cancellationToken)
    {
        return ProcessCreateClientPosition(request, cancellationToken);
    }

    private async Task<IActionResult> ProcessCreateClientPosition(CreateClientPositionCommand request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);
        return CreatedAtRoute("GetClientPositionById", new { id = response.Data.Id }, request);
    }
}