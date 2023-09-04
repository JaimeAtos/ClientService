using Application.Features.ClientPositionsManager.Commands.CreateClientPositionManager;
using Application.Features.ClientPositionsManager.Commands.DeleteClientPositionManagerCommand;
using Application.Features.ClientPositionsManager.Commands.UpdateClientPositionManagerCommand;
using Application.Features.ClientPositionsManager.Queries.GetAllClientPositionManagerQuery;
using Application.Features.ClientPositionsManager.Queries.GetClientPositionManagerByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers
{
    [ApiVersion("1.0")]
    public class ClientPositionManagerController : BaseApiController
    {
        public ClientPositionManagerController( IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult>CreateClientPositionManager(CreateClientPositionManagerCommand createClientPositionManager)
        {
            var result = await Mediator.Send(createClientPositionManager);
            return CreatedAtRoute("GetClientPositionManagerById", new {id = result.Data}, createClientPositionManager);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClientPositionManager(DeleteClientPositionManagerCommand deleteClientPositionManagerCommand, CancellationToken cancellationToken)
        {
            await Mediator.Send(deleteClientPositionManagerCommand, cancellationToken);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClientPositionManager(UpdateClientPositionManagerCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClientPositionManager([FromQuery]GetAllClientPositionManagerParameters filters)
        {
            return Ok(await Mediator.Send(new GetAllClientPositionManagerQuery() 
            {
                PageNumber = filters.PageNumber, 
                PageSize = filters.PageSize,
                Resource = filters.Resource,
                State = filters.State
            }));
        }

        [HttpGet("{id}", Name = "GetClientPositionManagerById")]
        public async Task<IActionResult> GetClientPositionManagerById(Guid id)
        {
            var result = await Mediator.Send(new GetClientPositionManagerByIdQuery() { Id = id });
            return Ok(result);
        }
    }
}
