using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.LeaveRequests.Queries.GetLeaveRequestByIdQuery
{
    public class GetLeaveRequestByIdQuery : IRequest<Response<LeaveRequestDTO>>
    {
        public Guid Id { get; set; }
    }

    public class GetLeaveRequestByIdQueryHandler : IRequestHandler<GetLeaveRequestByIdQuery, Response<LeaveRequestDTO>>
    {
        private readonly IRepositoryAsync<LeaveRequest> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetLeaveRequestByIdQueryHandler(IRepositoryAsync<LeaveRequest> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public Task<Response<LeaveRequestDTO>> Handle(GetLeaveRequestByIdQuery request,
            CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            return HandleProcess(request, cancellationToken);
        }

        private async Task<Response<LeaveRequestDTO>> HandleProcess(GetLeaveRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _repositoryAsync.GetByIdAsync(request.Id, cancellationToken);
            if (leaveRequest == null) throw new ApiExceptions($"register {request.Id} Not Found");
            var leaveRequestDto = _mapper.Map<LeaveRequestDTO>(leaveRequest);
            return new Response<LeaveRequestDTO>(leaveRequestDto);
        }
    }
}