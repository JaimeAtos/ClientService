using Application.Features.Clients.Commands.CreateClientCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers.v1.Clients;
[ApiVersion("1.0")]
public class CreateClientController : BaseApiController
{
    public CreateClientController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpPost]
    public Task<IActionResult> CreateClient(CreateClientCommand request, CancellationToken cancellationToken)
    {
        return ProcessCreateClient(request, cancellationToken);
    }

    private async Task<IActionResult> ProcessCreateClient(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);
        return CreatedAtRoute("GetClientById", new { id = response.Data.Id}, request);
    }
}