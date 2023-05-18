using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.ClientPosition.Commands.CreateClientPositionCommand
{
    public class CreateClientPositionCommand : IRequest<Response<Domain.Entities.ClientPosition>>
    {
        public Guid ClientId { get; set; }
        public Guid PositionId { get; set; }
        public string PositionDescription { get; set; } = null!;
        public Guid CurrentStateId { get; set; }
        public string CurrentStateName { get; set; } = null!;
    }

    public class CreateClientPositionCommandHandler : IRequestHandler<CreateClientPositionCommand, Response<Domain.Entities.ClientPosition>>
    {
        private readonly IRepositoryAsync<Domain.Entities.ClientPosition> _repositoryAsync;
        private readonly IMapper _mapper;
        public CreateClientPositionCommandHandler(IRepositoryAsync<Domain.Entities.ClientPosition> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public Task<Response<Domain.Entities.ClientPosition>> Handle(CreateClientPositionCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Create request is empty");
            }

            return HandleProcess(request, cancellationToken);
        }

        public async Task<Response<Domain.Entities.ClientPosition>> HandleProcess(CreateClientPositionCommand request, CancellationToken cancellationToken)
        {
           var clientPosition = _mapper.Map<Domain.Entities.ClientPosition>(request);

            await _repositoryAsync.AddAsync(clientPosition);
            return new Response<Domain.Entities.ClientPosition>(clientPosition);
        }
    }
}
