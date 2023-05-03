using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.ClientPositionManager.Commands.CreateClientPositionManager
{
    public class CreateClientPositionManagerCommand : IRequest<Response<Guid>>
    {
        public Guid ClientPositionId { get; set; }
        public Guid ResourceId { get; set; }
        public string Resource { get; set; }
    }

    public class CreateClientPositionManagerHandler : IRequestHandler<CreateClientPositionManagerCommand, Response<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryAsync<Domain.Entities.ClientPositionManager> _repositoryAsync;

        public CreateClientPositionManagerHandler(IMapper mapper, IRepositoryAsync<Domain.Entities.ClientPositionManager> repositoryAsync)
        {
            _mapper = mapper;
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<Guid>> Handle(CreateClientPositionManagerCommand request, CancellationToken cancellationToken)
        {
            var clientResponseManager = _mapper.Map<Domain.Entities.ClientPositionManager>(request);
            await _repositoryAsync.AddAsync(clientResponseManager);
            return new Response<Guid>(clientResponseManager.Id);
        }
    }
}       
