using Application.DTOs;
using Application.Interfaces;
using Application.Specification;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.LeaveRequests.Queries.GetAllLeaveRequestQuery
{
    public class GetAllLeaveRequestQuery : IRequest<PagedResponse<List<LeaveRequestDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid? ResourceId { get; set; }
        public string? LeaveReasonComments { get; set; }
        public bool State { get; set; }
    }

    public class GetAllLeaveRequestQueryHandler : IRequestHandler<GetAllLeaveRequestQuery, PagedResponse<List<LeaveRequestDTO>>>
    {
        private readonly IRepositoryAsync<LeaveRequest> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllLeaveRequestQueryHandler(IRepositoryAsync<LeaveRequest> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public Task<PagedResponse<List<LeaveRequestDTO>>> Handle(GetAllLeaveRequestQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            return HandleProcess(request, cancellationToken);
            
        }

        private async Task<PagedResponse<List<LeaveRequestDTO>>> HandleProcess(GetAllLeaveRequestQuery request, CancellationToken cancellationToken)
        {
            var pagination = new PagedLeaveRequestSpecification(request);
            var leaveRequests = await _repositoryAsync.ListAsync(pagination, cancellationToken);
            var leaveRequestsDto = _mapper.Map<List<LeaveRequestDTO>>(leaveRequests);
            return new PagedResponse<List<LeaveRequestDTO>>(leaveRequestsDto, request.PageNumber, request.PageSize);
        }
    }
}
