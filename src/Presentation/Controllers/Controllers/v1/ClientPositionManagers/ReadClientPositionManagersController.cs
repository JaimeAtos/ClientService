using Application.Features.ClientPositionsManager.Queries.GetAllClientPositionManagerQuery;
using Application.Features.ClientPositionsManager.Queries.GetClientPositionManagerByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers.v1.ClientPositionManagers;
[ApiVersion("1.0")]
public class ReadClientPositionManagersController : BaseApiController
{
    public ReadClientPositionManagersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("id", Name = "GetClientPositionManagerById")]
    public async Task<IActionResult> GetClientPositionManagerById(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetClientPositionManagerByIdQuery { Id = id }, cancellationToken));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCollaborators([FromQuery] GetAllClientPositionManagerParameters filters,
        CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAllClientPositionManagerQuery
        {
            PageNumber = filters.PageNumber, 
            PageSize = filters.PageSize,
            Resource = filters.Resource,
            State = filters.State
        }, cancellationToken));
    }
}