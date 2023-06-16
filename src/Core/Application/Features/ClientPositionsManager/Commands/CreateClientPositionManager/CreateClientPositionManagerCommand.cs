using Application.Wrappers;
using Ardalis.Specification;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ClientPositionsManager.Commands.CreateClientPositionManager
{
    public class CreateClientPositionManagerCommand : IRequest<Response<Guid>>
    {
        public Guid ClientPositionId { get; set; }
        public Guid ResourceId { get; set; }
        public string Resource { get; set; }
    }

    public class
        CreateClientPositionManagerHandler : IRequestHandler<CreateClientPositionManagerCommand, Response<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<ClientPositionManager> _repositoryAsync;

        public CreateClientPositionManagerHandler(IMapper mapper,
            IRepositoryBase<ClientPositionManager> repositoryAsync)
        {
            _mapper = mapper;
            _repositoryAsync = repositoryAsync;
        }

        public Task<Response<Guid>> Handle(CreateClientPositionManagerCommand request,
            CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request), "Create request is empty");
            return ProcessHandle(request, cancellationToken);
        }

        private async Task<Response<Guid>> ProcessHandle(CreateClientPositionManagerCommand request,
            CancellationToken cancellationToken)
        {
            var clientResponseManager = _mapper.Map<ClientPositionManager>(request);
            await _repositoryAsync.AddAsync(clientResponseManager, cancellationToken);
            return new Response<Guid>(clientResponseManager.Id);
        }
    }
}