using Application.Features.Clients.Queries.GetAllClients;
using Application.Features.Clients.Queries.GetClientById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers.v1.Clients;
[ApiVersion("1.0")]
public class ReadClientsController : BaseApiController
{
    public ReadClientsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{id}", Name = "GetClientById")]
    public async Task<IActionResult> GetClientById(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetClientByIdQuery { Id = id }, cancellationToken));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCollaborators([FromQuery] GetAllClientsParameters filters,
        CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAllClientsQuery
        {
            PageNumber = filters.PageNumber,
            PageSize = filters.PageSize,
            Name = filters.Name,
            LocationId = filters.LocationId,
            LocationName = filters.LocationName,
            CountPositions = filters.CountPositions,
            State = filters.State
        }, cancellationToken));
    }
}