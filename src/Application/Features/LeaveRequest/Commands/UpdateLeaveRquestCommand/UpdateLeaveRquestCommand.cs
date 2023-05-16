using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveRequest.Commands.UpdateLeaveRquestCommand
{
    public class UpdateLeaveRquestCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public Guid ClientPositionId { get; set; }
        public Guid ResourceId { get; set; }
        public Guid ReasonId { get; set; }
        public string? LeaveReasonComments { get; set; }
    }

    public class UpdateLeaveRquestCommandHandler : IRequestHandler<UpdateLeaveRquestCommand, Response<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryAsync<Domain.Entities.LeaveRequest> _repositoryAsync;

        public UpdateLeaveRquestCommandHandler(IMapper mapper, IRepositoryAsync<Domain.Entities.LeaveRequest> repositoryAsync)
        {
            _mapper = mapper;
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<Guid>> Handle(UpdateLeaveRquestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _repositoryAsync.GetByIdAsync(request.Id);
            if(leaveRequest == null)
            {
                throw new ApiExceptions($"register {request.Id} Not Found");
            }
            else
            {
                leaveRequest.Id = request.Id;
                leaveRequest.ClientPositionId = request.ClientPositionId;
                leaveRequest.ResourceId = request.ResourceId;
                leaveRequest.ReasonId = request.ReasonId;
                leaveRequest.LeaveReasonComments = request.LeaveReasonComments;

                await _repositoryAsync.UpdateAsync(leaveRequest);
                return new Response<Guid>(leaveRequest.Id);
            }
        }
    }
}
