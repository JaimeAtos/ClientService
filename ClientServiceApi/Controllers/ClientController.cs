using Application.Features.Client.Commands.CreateClientCommand;
using Application.Features.Client.Commands.DeleteClientCommand;
using Application.Features.Client.Commands.UpdateClientCommnad;
using Application.Features.Client.Queries.GetAllClients;
using Application.Features.Client.Queries.GetClientById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClientServiceApi.Controllers
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
        public async Task<IActionResult> GetAllClients()
        {
            var response = await _mediator.Send(new GetAllClientsQuery());
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
