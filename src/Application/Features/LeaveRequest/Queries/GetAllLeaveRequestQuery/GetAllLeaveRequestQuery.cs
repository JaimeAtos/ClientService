using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Specification;

namespace Application.Features.LeaveRequest.Queries.GetAllLeaveRequestQuery
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
        private readonly IRepositoryAsync<Domain.Entities.LeaveRequest> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllLeaveRequestQueryHandler(IRepositoryAsync<Domain.Entities.LeaveRequest> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<PagedResponse<List<LeaveRequestDTO>>> Handle(GetAllLeaveRequestQuery request, CancellationToken cancellationToken)
        {
            var pagination = new PagedLeaveRequestSpecification(request);
            var leaveRequests = await _repositoryAsync.ListAsync(pagination);
            var leaveRequestsDto = _mapper.Map<List<LeaveRequestDTO>>(leaveRequests);
            return new PagedResponse<List<LeaveRequestDTO>>(leaveRequestsDto, request.PageNumber, request.PageSize);

        }
    }
}
