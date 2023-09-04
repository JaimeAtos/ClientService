using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.LeaveRequests.Commands.UpdateLeaveRquestCommand
{
    public class UpdateLeaveRequestCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public Guid ClientPositionId { get; set; }
        public Guid ResourceId { get; set; }
        public Guid ReasonId { get; set; }
        public string LeaveReasonComments { get; set; } = null!;
    }

    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Response<Guid>>
    {
        private readonly ILeaveRequestRepository _repositoryAsync;
        private readonly IMapper _mapper;

        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public Task<Response<Guid>> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            return HandleProcess(request, cancellationToken);
        }

        private async Task<Response<Guid>> HandleProcess(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _repositoryAsync.GetByIdAsync(request.Id, cancellationToken);
            if (leaveRequest == null) throw new ApiExceptions($"register {request.Id} Not Found");

            leaveRequest = _mapper.Map(request, leaveRequest);

            await _repositoryAsync.UpdateAsync(leaveRequest, cancellationToken);
            return new Response<Guid>(leaveRequest.Id);
        }
    }
}