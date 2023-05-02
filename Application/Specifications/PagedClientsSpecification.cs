using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class PagedClientsSpecification : Specification<Client>
    {
        public PagedClientsSpecification(int page, int pageSize)
        {

            Query.Skip((page - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
