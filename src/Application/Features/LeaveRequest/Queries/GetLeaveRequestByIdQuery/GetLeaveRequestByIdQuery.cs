using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveRequest.Queries.GetLeaveRequestByIdQuery
{
    public class GetLeaveRequestByIdQuery : IRequest<Response<LeaveRequestDTO>>
    {
        public Guid Id { get; set; }
    }

    public class GetLeaveRequestByIdQueryHandler : IRequestHandler<GetLeaveRequestByIdQuery, Response<LeaveRequestDTO>>
    {
        private readonly IRepositoryAsync<Domain.Entities.LeaveRequest> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetLeaveRequestByIdQueryHandler(IRepositoryAsync<Domain.Entities.LeaveRequest> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<LeaveRequestDTO>> Handle(GetLeaveRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _repositoryAsync.GetByIdAsync(request.Id);
            if(leaveRequest == null)
            {
                throw new ApiExceptions($"register {request.Id} Not Found");
            }
            else
            {
                var leaveRequestDTO = _mapper.Map<LeaveRequestDTO>(leaveRequest);
                return new Response<LeaveRequestDTO>(leaveRequestDTO);
            }
        }
    }
}
