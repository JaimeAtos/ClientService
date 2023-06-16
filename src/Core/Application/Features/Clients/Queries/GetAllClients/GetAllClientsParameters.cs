using Application.Parameters;

namespace Application.Features.Clients.Queries.GetAllClients;

public class GetAllClientsParameters : RequestParameter
{
    public string? Name { get; set; }
    public Guid? LocationId { get; set; }
    public string? LocationName { get; set; }
    public int? CountPositions { get; set; }
    public bool State { get; set; }
}