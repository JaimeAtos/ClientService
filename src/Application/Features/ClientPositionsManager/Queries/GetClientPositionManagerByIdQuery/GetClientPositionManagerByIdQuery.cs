using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.ClientPositionsManager.Queries.GetClientPositionManagerByIdQuery
{
    public class GetClientPositionManagerByIdQuery : IRequest<Response<ClientPositionManagerDTO>>
    {
        public Guid Id { get; set; }
    }

    public class GetClientPositionManagerByIdQueryHandler : IRequestHandler<GetClientPositionManagerByIdQuery, Response<ClientPositionManagerDTO>>
    {
        private readonly IRepositoryAsync<Domain.Entities.ClientPositionManager> _repositoryAsync;
        private readonly IMapper _mapper;
        public GetClientPositionManagerByIdQueryHandler(IRepositoryAsync<Domain.Entities.ClientPositionManager> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public Task<Response<ClientPositionManagerDTO>> Handle(GetClientPositionManagerByIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request),"Request mustn't be empty");
            return ProcessHandle(request, cancellationToken);
            
        }

        private async Task<Response<ClientPositionManagerDTO>> ProcessHandle(GetClientPositionManagerByIdQuery request, CancellationToken cancellationToken)
        {
            var clientResponseManager = await _repositoryAsync.GetByIdAsync(request.Id, cancellationToken);
            
            var clientResponseManagerDto = _mapper.Map<ClientPositionManagerDTO>(clientResponseManager);
            return new Response<ClientPositionManagerDTO>(clientResponseManagerDto);
        }
    }

}
