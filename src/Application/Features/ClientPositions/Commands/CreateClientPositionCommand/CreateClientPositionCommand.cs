using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ClientPositions.Commands.CreateClientPositionCommand
{
    public class CreateClientPositionCommand : IRequest<Response<ClientPosition>>
    {
        public Guid ClientId { get; set; }
        public Guid PositionId { get; set; }
        public string PositionDescription { get; set; } = null!;
        public Guid CurrentStateId { get; set; }
        public string CurrentStateName { get; set; } = null!;
    }

    public class
        CreateClientPositionCommandHandler : IRequestHandler<CreateClientPositionCommand, Response<ClientPosition>>
    {
        private readonly IRepositoryAsync<ClientPosition> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateClientPositionCommandHandler(IRepositoryAsync<ClientPosition> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public Task<Response<ClientPosition>> Handle(CreateClientPositionCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Create request is empty");

            return HandleProcess(request, cancellationToken);
        }

        private async Task<Response<ClientPosition>> HandleProcess(CreateClientPositionCommand request,
            CancellationToken cancellationToken)
        {
            var clientPosition = _mapper.Map<ClientPosition>(request);

            await _repositoryAsync.AddAsync(clientPosition, cancellationToken);
            return new Response<ClientPosition>(clientPosition);
        }
    }
}