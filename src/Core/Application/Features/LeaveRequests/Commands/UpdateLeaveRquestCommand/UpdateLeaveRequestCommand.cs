using Application.Exceptions;
using Application.Wrappers;
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

    public class UpdateLeaveRquestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Response<Guid>>
    {
        private readonly ILeaveRequestRepository _repositoryAsync;

        public UpdateLeaveRquestCommandHandler(ILeaveRequestRepository repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
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
            
            leaveRequest.Id = request.Id;
            leaveRequest.ClientPositionId = request.ClientPositionId;
            leaveRequest.ResourceId = request.ResourceId;
            leaveRequest.ReasonId = request.ReasonId;
            leaveRequest.LeaveReasonComments = request.LeaveReasonComments;

            await _repositoryAsync.UpdateAsync(leaveRequest, cancellationToken);
            return new Response<Guid>(leaveRequest.Id);
        }
    }
}