using Application.Features.Clients.Commands.CreateClientCommand;
using Application.Features.Clients.Commands.DeleteClientCommand;
using Application.Features.Clients.Commands.UpdateClientCommnad;
using Application.Features.Clients.Queries.GetAllClients;
using Application.Features.Clients.Queries.GetClientById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers
{
    [ApiVersion("1.0")]
    public class ClientController : BaseApiController
    {
        public ClientController(IMediator mediator) : base(mediator)
        {
            
        }
       
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientCommand createClientCommand)
        {
            var response = await Mediator.Send(createClientCommand);
            return CreatedAtRoute("GetClientById", new { id = response.Data.Id }, createClientCommand);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClient([FromBody] DeleteClientCommand deleteClientCommand, CancellationToken cancellationToken)
        {
            await Mediator.Send(deleteClientCommand, cancellationToken);
            return NoContent();
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateClient([FromBody] UpdateClientCommand updateClientCommand, CancellationToken cancellationToken)
        {
            await Mediator.Send(updateClientCommand, cancellationToken);
            return NoContent();
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllClients([FromQuery]GetAllClientsParameters filters)
        {
            return Ok(await Mediator.Send(new GetAllClientsQuery() 
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

        [HttpGet("{id}",Name = "GetClientById")]
        public async Task<IActionResult> GetClientById(Guid id)
        {
            var response = await Mediator.Send(new GetClientByIdQuery() { Id = id});
            return Ok(response);
        }
        
    }
}
