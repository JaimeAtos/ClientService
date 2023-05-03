using Application.Features.ClientPositionManager.Queries.GetAllClientPositionManagerQuery;
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specification;

public class PagedClientPositionManagerSpecification : Specification<ClientPositionManager>
{
    public PagedClientPositionManagerSpecification(GetAllClientPositionManagerQuery request)
    {
        Query.Skip((request.PageNumber) * request.PageSize)
            .Take(request.PageSize);
        
        if (!string.IsNullOrEmpty(request.Resource))
            Query.Search(x => x.Resource, "%" + request.Resource + "%");
        
        Query.Search(x => x.State.ToString(), request.State.ToString());
    }
}