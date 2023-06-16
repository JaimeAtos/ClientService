using Application.Parameters;

namespace Application.Features.ClientPositionsManager.Queries.GetAllClientPositionManagerQuery;

public class GetAllClientPositionManagerParameters : RequestParameter
{
    public string? Resource { get; set; }
    public bool State { get; set; }
}