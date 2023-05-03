using Application.Parameters;

namespace Application.Features.ClientPosition.Queries.GetAllClientPositionsQuery;

public class GetAllClientPositionsParameters : RequestParameter
{
    public string? PositionDescription { get; set; }
    public string? CurrentStateName { get; set; }
    public bool State { get; set; }
}