using Application.Features.ClientPositions.Queries.GetAllClientPositionsQuery;
using Application.Features.ClientPositions.Queries.GetClientPositionByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers.v1.ClientPositions;
[ApiVersion("1.0")]
public class ReadClientPositionsController : BaseApiController
{
    public ReadClientPositionsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("id", Name = "GetClientPositionById")]
    public async Task<IActionResult> GetClientPositionById(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetClientPositionByIdQuery { Id = id }, cancellationToken));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCollaborators([FromQuery] GetAllClientPositionsParameters filters,
        CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAllClientPositionsQuery
        {
            PageNumber = filters.PageNumber,
            PageSize = filters.PageSize,
            PositionDescription = filters.PositionDescription,
            CurrentStateName = filters.CurrentStateName,
            State = filters.State
        }, cancellationToken));
    }
}