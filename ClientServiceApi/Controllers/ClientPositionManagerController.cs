using Application.Features.Client.Commands.UpdateClientCommnad;
using Application.Features.ClientPositionManager.Commands.CreateClientPositionManager;
using Application.Features.ClientPositionManager.Commands.DeleteClientPositionCommand;
using Application.Features.ClientPositionManager.Commands.UpdateClientPositionManagerCommand;
using Application.Features.ClientPositionManager.Queries.GetAllClientPositionManagerQuery;
using Application.Features.ClientPositionManager.Queries.GetClientPositionManagerByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ClientServiceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientPositionManagerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientPositionManagerController( IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/CreateClientPositionManager")]
        public async Task<IActionResult>CreateClientPositionManager(CreateClientPositionManagerCommand createClientPositionManager)
        {
            var result = await _mediator.Send(createClientPositionManager);
            return CreatedAtRoute("GetClientPositionManagerById", new {id = result.Data}, createClientPositionManager);
        }

        [HttpDelete("/DeleteClientPositionManager")]
        public async Task<IActionResult> DeleteClientPositionManager(DeleteClientPositionManagerCommand deleteClientPositionManagerCommand)
        {
            await _mediator.Send(deleteClientPositionManagerCommand);
            return NoContent();
        }

        [HttpPut("/UpdateClientPositionManager")]
        public async Task<IActionResult> UpdateClientPositionManager(UpdateClientPositionManagerCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("/GetAllClientPositionManager")]
        public async Task<IActionResult> GetAllClientPositionManager()
        {
            var result = await _mediator.Send( new GetAllClientPositionManagerQuery () );
            return Ok(result);
        }

        [HttpGet("/GetClientPositionManagerById/{id}", Name = "GetClientPositionManagerById")]
        public async Task<IActionResult> GetClientPositionManagerById(Guid id)
        {
            var result = await _mediator.Send(new GetClientPositionManagerByIdQuery() { Id = id });
            return Ok(result);
        }
    }
}
