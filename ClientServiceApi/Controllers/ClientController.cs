using Application.Features.Client.Commands.CreateClientCommand;
using Application.Features.Client.Commands.DeleteClientCommand;
using Application.Features.Client.Commands.UpdateClientCommnad;
using Application.Features.Client.Queries.GetAllClients;
using Application.Features.Client.Queries.GetClientById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistence.Context;

namespace ClientServiceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ClientContext _clientContext;
        public ClientController(IMediator mediator, ClientContext clientContext)
        {
            _mediator = mediator;
            _clientContext = clientContext;
       
        }
       
        [HttpPost("/CreateClient")]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientCommand createClientCommand)
        {
           //THIS CODE WORKS BUT IT WILL NOT BE APPLIED
            //var existingClient = await _clientContext.Clients.FirstOrDefaultAsync(c => c.Name == createClientCommand.Name);

            //if (existingClient != null)
            //{
            //    return Conflict("This client already exist");
            //}
            var response = await _mediator.Send(createClientCommand);
            return CreatedAtRoute("GetClientById", new { id = response.Data.Id }, createClientCommand); ;
        }

        [HttpDelete("/DeleteClient")]
        public async Task<IActionResult> DeleteClient([FromBody] DeleteClientCommand deleteClientCommand)
        {
            var response = await _mediator.Send(deleteClientCommand);
            return NoContent();
        }

 
        [HttpPut("/UpdateClient")]
        public async Task<IActionResult> UpdateClient([FromBody] UpdateClientCommand updateClientCommand)
        {
            var response = await _mediator.Send(updateClientCommand);
            return Ok(response);
        }
        [HttpGet("/GetAllClients")]
        public async Task<IActionResult> GetAllClients([FromQuery] GetAllClientsParameter filter)
        {
            var response = await _mediator.Send(new GetAllClientsQuery { 
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            });
            return Ok(response);
        }

        [HttpGet("/GetClientById/{id}",Name = "GetClientById")]
        public async Task<IActionResult> GetClientById(Guid id)
        {
            var response = await _mediator.Send(new GetClientByIdQuery() { Id = id});
            return Ok(response);
        }


    }
}
