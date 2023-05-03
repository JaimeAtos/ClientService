using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.ClientPositionManager.Queries.GetClientPositionManagerByIdQuery
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

        public async Task<Response<ClientPositionManagerDTO>> Handle(GetClientPositionManagerByIdQuery request, CancellationToken cancellationToken)
        {
            var clientResponseManager = await _repositoryAsync.GetByIdAsync(request.Id);
            var clientResponseManagerDTO = _mapper.Map<ClientPositionManagerDTO>(clientResponseManager);
            return new Response<ClientPositionManagerDTO>(clientResponseManagerDTO);
        }
    }

}
