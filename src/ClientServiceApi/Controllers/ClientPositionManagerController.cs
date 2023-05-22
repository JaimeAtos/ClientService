using Application.Features.ClientPositionsManager.Commands.CreateClientPositionManager;
using Application.Features.ClientPositionsManager.Commands.DeleteClientPositionManagerCommand;
using Application.Features.ClientPositionsManager.Commands.UpdateClientPositionManagerCommand;
using Application.Features.ClientPositionsManager.Queries.GetAllClientPositionManagerQuery;
using Application.Features.ClientPositionsManager.Queries.GetClientPositionManagerByIdQuery;
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
        public async Task<IActionResult> DeleteClientPositionManager(DeleteClientPositionManagerCommand deleteClientPositionManagerCommand, CancellationToken cancellationToken)
        {
            await _mediator.Send(deleteClientPositionManagerCommand, cancellationToken);
            return NoContent();
        }

        [HttpPut("/UpdateClientPositionManager")]
        public async Task<IActionResult> UpdateClientPositionManager(UpdateClientPositionManagerCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpGet("/GetAllClientPositionManager")]
        public async Task<IActionResult> GetAllClientPositionManager([FromQuery]GetAllClientPositionManagerParameters filters)
        {
            return Ok(await _mediator.Send(new GetAllClientPositionManagerQuery() 
            {
                PageNumber = filters.PageNumber, 
                PageSize = filters.PageSize,
                Resource = filters.Resource,
                State = filters.State
            }));
        }

        [HttpGet("/GetClientPositionManagerById/{id}", Name = "GetClientPositionManagerById")]
        public async Task<IActionResult> GetClientPositionManagerById(Guid id)
        {
            var result = await _mediator.Send(new GetClientPositionManagerByIdQuery() { Id = id });
            return Ok(result);
        }
    }
}
