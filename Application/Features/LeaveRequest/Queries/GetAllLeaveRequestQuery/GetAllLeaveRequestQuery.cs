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

namespace Application.Features.LeaveRequest.Queries.GetAllLeaveRequestQuery
{
    public class GetAllLeaveRequestQuery : IRequest<Response<List<LeaveRequestDTO>>>
    {
    }

    public class GetAllLeaveRequestQueryHandler : IRequestHandler<GetAllLeaveRequestQuery, Response<List<LeaveRequestDTO>>>
    {
        private readonly IRepositoryAsync<Domain.Entities.LeaveRequest> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllLeaveRequestQueryHandler(IRepositoryAsync<Domain.Entities.LeaveRequest> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<List<LeaveRequestDTO>>> Handle(GetAllLeaveRequestQuery request, CancellationToken cancellationToken)
        {
            var leaveRequests = await _repositoryAsync.ListAsync();
            var leaveRequestsDTO = _mapper.Map<List<LeaveRequestDTO>>(leaveRequests);
            return new Response<List<LeaveRequestDTO>>(leaveRequestsDTO);

        }
    }
}
