using Application.Features.Clients.Commands.CreateClientCommand;
using Application.Features.Clients.Commands.DeleteClientCommand;
using Application.Features.Clients.Commands.UpdateClientCommnad;
using Application.Features.Clients.Queries.GetAllClients;
using Application.Features.Clients.Queries.GetClientById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }
       
        [HttpPost("/CreateClient")]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientCommand createClientCommand)
        {
            var response = await _mediator.Send(createClientCommand);
            return CreatedAtRoute("GetClientById", new { id = response.Data.Id }, createClientCommand);
        }

        [HttpDelete("/DeleteClient")]
        public async Task<IActionResult> DeleteClient([FromBody] DeleteClientCommand deleteClientCommand, CancellationToken cancellationToken)
        {
            await _mediator.Send(deleteClientCommand, cancellationToken);
            return NoContent();
        }
        
        [HttpPut("/UpdateClient")]
        public async Task<IActionResult> UpdateClient([FromBody] UpdateClientCommand updateClientCommand, CancellationToken cancellationToken)
        {
            await _mediator.Send(updateClientCommand, cancellationToken);
            return NoContent();
        }
        
        [HttpGet("/GetAllClients")]
        public async Task<IActionResult> GetAllClients([FromQuery]GetAllClientsParameters filters)
        {
            return Ok(await _mediator.Send(new GetAllClientsQuery() 
            {
                PageNumber = filters.PageNumber, 
                PageSize = filters.PageSize,
                Name = filters.Name,
                LocationId = filters.LocationId,
                LocationName = filters.LocationName,
                CountPositions = filters.CountPositions,
                State = filters.State
            }));
        }

        [HttpGet("/GetClientById/{id}",Name = "GetClientById")]
        public async Task<IActionResult> GetClientById(Guid id)
        {
            var response = await _mediator.Send(new GetClientByIdQuery() { Id = id});
            return Ok(response);
        }
        
    }
}
