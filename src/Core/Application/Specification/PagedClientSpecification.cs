using Application.Features.Clients.Queries.GetAllClients;
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specification;

public class PagedClientSpecification : Specification<Client>
{
    public PagedClientSpecification(GetAllClientsQuery request)
    {
        Query.Skip((request.PageNumber) * request.PageSize)
            .Take(request.PageSize);

        Query.Include(c => c.ClientPositions);

        if (request.Id is null)
            Query.Include(c => c.ClientPositions);
        else
            Query.Include(c => c.ClientPositions).Where(c => c.Id == request.Id);

        if (!string.IsNullOrEmpty(request.Name))
            Query.Search(x => x.Name, "%" + request.Name + "%");

        if (!string.IsNullOrEmpty(request.LocationId.ToString()))
            Query.Search(x => x.LocationId.ToString(), "%" + request.LocationId + "%");

        if (!string.IsNullOrEmpty(request.LocationName))
            Query.Search(x => x.LocationName, "%" + request.LocationName + "%");

        Query.Search(x => x.State.ToString(), request.State.ToString());
    }
}