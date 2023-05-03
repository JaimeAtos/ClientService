using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;

namespace Application.Features.LeaveRequest.Commands.DeleteLeaveRequestCommand
{
    public class DeleteLeaveRequestCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Domain.Entities.LeaveRequest> _repositoryAsync;

        public DeleteLeaveRequestCommandHandler(IRepositoryAsync<Domain.Entities.LeaveRequest> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<Guid>> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _repositoryAsync.GetByIdAsync(request.Id);
            if(leaveRequest == null)
            {
                throw new ApiExceptions($"register {request.Id} Not Found");
            }
            else
            {
                leaveRequest.State = false;
                await _repositoryAsync.UpdateAsync(leaveRequest);
                return new Response<Guid>(leaveRequest.Id);
            }
        }
    }
}
