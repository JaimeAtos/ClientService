using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveRequest.Commands.CreateLeaveRequestCommand
{
    public class CreateLeaveRequestCommand : IRequest<Response<Guid>>
    {
        public Guid PositionId { get; set; }
        public Guid ResourceId { get; set; }
        public Guid ReasonId { get; set; }
        public string? leaveReasonComments { get; set; }
    }

    public class CreateLeaveRequestHandler : IRequestHandler<CreateLeaveRequestCommand, Response<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryAsync<Domain.Entities.LeaveRequest> _repositoryAsync;

        public CreateLeaveRequestHandler(IMapper mapper, IRepositoryAsync<Domain.Entities.LeaveRequest> repositoryAsync)
        {
            _mapper = mapper;
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<Guid>> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = _mapper.Map<Domain.Entities.LeaveRequest>(request);
            await _repositoryAsync.AddAsync(leaveRequest);
            return new Response<Guid>(leaveRequest.Id);
        }
    }
}
