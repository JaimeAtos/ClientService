using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.LeaveRequests.Commands.CreateLeaveRequestCommand
{
    public class CreateLeaveRequestCommand : IRequest<Response<Guid>>
    {
        public Guid PositionId { get; set; }
        public Guid ResourceId { get; set; }
        public Guid ReasonId { get; set; }
        public string? LeaveReasonComments { get; set; }
    }

    public class CreateLeaveRequestHandler : IRequestHandler<CreateLeaveRequestCommand, Response<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _repositoryAsync;

        public CreateLeaveRequestHandler(IMapper mapper, ILeaveRequestRepository repositoryAsync)
        {
            _mapper = mapper;
            _repositoryAsync = repositoryAsync;
        }

        public Task<Response<Guid>> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            return HandleProcess(request, cancellationToken);
        }

        private async Task<Response<Guid>> HandleProcess(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = _mapper.Map<LeaveRequest>(request);
            await _repositoryAsync.AddAsync(leaveRequest, cancellationToken);
            return new Response<Guid>(leaveRequest.Id);
        }
    }
}
