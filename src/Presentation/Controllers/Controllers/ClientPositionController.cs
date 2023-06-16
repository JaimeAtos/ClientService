using Application.Features.ClientPositions.Commands.CreateClientPositionCommand;
using Application.Features.ClientPositions.Commands.DeleteClientPositionCommand;
using Application.Features.ClientPositions.Commands.UpdateClientPositionCommnad;
using Application.Features.ClientPositions.Queries.GetAllClientPositionsQuery;
using Application.Features.ClientPositions.Queries.GetClientPositionByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers
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
            return CreatedAtRoute("GetClientPositionById", new { id = response.Data.Id}, createClientPositionCommand);
        }

        [HttpDelete("/DeleteClientPosition")]
        public async Task<IActionResult> DeleteClientPosition([FromBody] DeleteClientPositionCommand deleteClientPositionCommand)
        {
            await _mediator.Send(deleteClientPositionCommand);
            return NoContent();
        }

        [HttpPut("/UpdateClientPosition")]
        public async Task<IActionResult> UpdateClientPosition([FromBody] UpdateClientPositionCommnad updateClientPositionCommand)
        {
            await _mediator.Send(updateClientPositionCommand);
            return NoContent();
        }

        [HttpGet("/GetAllClientPositions")]
        public async Task<IActionResult> GetAllClientPositions([FromQuery]GetAllClientPositionsParameters filters)
        {
            return Ok(await _mediator.Send(new GetAllClientPositionsQuery() 
            {
                PageNumber = filters.PageNumber, 
                PageSize = filters.PageSize,
                PositionDescription = filters.PositionDescription,
                CurrentStateName = filters.CurrentStateName,
                State = filters.State
            }));
        }

        [HttpGet("/GetClientPositionById/{id}", Name = "GetClientPositionById")]
        public async Task<IActionResult> GetClientPositionById(Guid id)
        {
            var response = await _mediator.Send(new GetClientPositionByIdQuery() { Id = id});
            return Ok(response);
        }
    }
}
