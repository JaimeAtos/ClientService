using Application.Features.ClientPositions.Commands.CreateClientPositionCommand;
using Application.Features.ClientPositions.Commands.DeleteClientPositionCommand;
using Application.Features.ClientPositions.Commands.UpdateClientPositionCommnad;
using Application.Features.ClientPositions.Queries.GetAllClientPositionsQuery;
using Application.Features.ClientPositions.Queries.GetClientPositionByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers
{
    [ApiVersion("1.0")]
    public class ClientPositionController : BaseApiController
    {
        public ClientPositionController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateClientPosition(
            [FromBody] CreateClientPositionCommand createClientPositionCommand)
        {
            var response = await Mediator.Send(createClientPositionCommand);
            return CreatedAtRoute("GetClientPositionById", new { id = response.Data.Id }, createClientPositionCommand);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClientPosition(
            [FromBody] DeleteClientPositionCommand deleteClientPositionCommand)
        {
            await Mediator.Send(deleteClientPositionCommand);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClientPosition(
            [FromBody] UpdateClientPositionCommand updateClientPositionCommand)
        {
            await Mediator.Send(updateClientPositionCommand);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClientPositions([FromQuery] GetAllClientPositionsParameters filters)
        {
            return Ok(await Mediator.Send(new GetAllClientPositionsQuery()
            {
                PageNumber = filters.PageNumber,
                PageSize = filters.PageSize,
                PositionDescription = filters.PositionDescription,
                CurrentStateName = filters.CurrentStateName,
                State = filters.State
            }));
        }

        [HttpGet("{id}", Name = "GetClientPositionById")]
        public async Task<IActionResult> GetClientPositionById(Guid id)
        {
            var response = await Mediator.Send(new GetClientPositionByIdQuery() { Id = id });
            return Ok(response);
        }
    }
}