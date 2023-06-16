using Application.Features.ClientPositions.Queries.GetAllClientPositionsQuery;
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specification;

public class PagedClientPositionSpecification : Specification<ClientPosition>
{
    public PagedClientPositionSpecification(GetAllClientPositionsQuery request)
    {
        Query.Skip((request.PageNumber) * request.PageSize)
            .Take(request.PageSize);
        
        if (!string.IsNullOrEmpty(request.PositionDescription))
            Query.Search(x => x.PositionDescription, "%" + request.PositionDescription + "%");
        
        if (!string.IsNullOrEmpty(request.CurrentStateName))
            Query.Search(x => x.CurrentStateName, "%" + request.CurrentStateName + "%");
        
        Query.Search(x => x.State.ToString(), request.State.ToString());
    }
}