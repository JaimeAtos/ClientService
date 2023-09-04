using Application.Features.Clients.Commands.DeleteClientCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers.v1.Clients;
[ApiVersion("1.0")]
public class DeleteClientController: BaseApiController
{
    public DeleteClientController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpDelete]
    public Task<IActionResult> DeleteClient(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        return ProcessDeleteClient(request, cancellationToken);
    }

    private async Task<IActionResult> ProcessDeleteClient(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }
}