using Application.DTOs;
using Application.Exceptions;
using Application.Wrappers;
using Ardalis.Specification;
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
        private readonly IRepositoryBase<LeaveRequest> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetLeaveRequestByIdQueryHandler(IRepositoryBase<LeaveRequest> repositoryAsync, IMapper mapper)
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