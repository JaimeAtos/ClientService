using Application.Features.ClientPosition.Commands.CreateClientPositionCommand;
using Application.Features.ClientPosition.Commands.DeleteClientPositionCommand;
using Application.Features.ClientPosition.Commands.UpdateClientPositionCommnad;
using Application.Features.ClientPosition.Queries.GetAllClientPositionsQuery;
using Application.Features.ClientPosition.Queries.GetClientPositionByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClientServiceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientPositionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClientPositionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/CreateClientPosition")]
        public async Task<IActionResult> CreateClientPosition([FromBody] CreateClientPositionCommand createClientPositionCommand)
        {
            var response = await _mediator.Send(createClientPositionCommand);
            return Ok(response);
        }

        [HttpDelete("/DeleteClientPosition")]
        public async Task<IActionResult> DeleteClientPosition([FromBody] DeleteClientPositionCommand deleteClientPositionCommand)
        {
            var response = await _mediator.Send(deleteClientPositionCommand);
            return Ok(response);
        }

        [HttpPut("/UpdateClientPosition")]
        public async Task<IActionResult> UpdateClientPosition([FromBody] UpdateClientPositionCommnad updateClientPositionCommand)
        {
            var response = await _mediator.Send(updateClientPositionCommand);
            return Ok(response);
        }

        [HttpGet("/GetAllClientPositions")]
        public async Task<IActionResult> GetAllClientPositions()
        {
            var response = await _mediator.Send(new GetAllClientPositionsQuery());
            return Ok(response);
        }

        [HttpGet("/GetClientPositionById/{id}")]
        public async Task<IActionResult> GetClientPositionById(Guid id)
        {
            var response = await _mediator.Send(new GetClientPositionByIdQuery() { Id = id});
            return Ok(response);
        }
    }
}
