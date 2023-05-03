using Application.Features.LeaveRequest.Queries.GetAllLeaveRequestQuery;
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specification;

public class PagedLeaveRequestSpecification : Specification<LeaveRequest>
{
    public PagedLeaveRequestSpecification(GetAllLeaveRequestQuery request)
    {
        Query.Skip((request.PageNumber) * request.PageSize)
            .Take(request.PageSize);
        
        if (!string.IsNullOrEmpty(request.ResourceId.ToString()))
            Query.Search(x => x.ResourceId.ToString(), "%" + request.ResourceId + "%");
        
        if (!string.IsNullOrEmpty(request.LeaveReasonComments))
            Query.Search(x => x.LeaveReasonComments, "%" + request.LeaveReasonComments + "%");
        
        Query.Search(x => x.State.ToString(), request.State.ToString());
    }
}