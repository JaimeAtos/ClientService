using Application.Exceptions;
using Application.Wrappers;
using Ardalis.Specification;
using Domain.Entities;
using MediatR;

namespace Application.Features.LeaveRequests.Commands.DeleteLeaveRequestCommand
{
    public class DeleteLeaveRequestCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Response<Guid>>
    {
        private readonly IRepositoryBase<LeaveRequest> _repositoryAsync;

        public DeleteLeaveRequestCommandHandler(IRepositoryBase<LeaveRequest> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public Task<Response<Guid>> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            return HandleProcess(request, cancellationToken);
        }

        private async Task<Response<Guid>> HandleProcess(DeleteLeaveRequestCommand request,
            CancellationToken cancellationToken)
        {
            var leaveRequest = await _repositoryAsync.GetByIdAsync(request.Id, cancellationToken);
            if (leaveRequest == null) throw new ApiExceptions($"register {request.Id} Not Found");
            leaveRequest.State = false;
            await _repositoryAsync.UpdateAsync(leaveRequest, cancellationToken);
            return new Response<Guid>(leaveRequest.Id);
        }
    }
}