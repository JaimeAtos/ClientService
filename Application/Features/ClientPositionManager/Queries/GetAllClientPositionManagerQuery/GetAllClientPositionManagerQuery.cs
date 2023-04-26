using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.ClientPositionManager.Queries.GetAllClientPositionManagerQuery
{
    public class GetAllClientPositionManagerQuery : IRequest<Response<List<ClientPositionManagerDTO>>>
    {
    }

    public class GetAllClientPositionManagerHandler : IRequestHandler<GetAllClientPositionManagerQuery, Response<List<ClientPositionManagerDTO>>>
    {
        private readonly IRepositoryAsync<Domain.Entities.ClientPositionManager> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllClientPositionManagerHandler(IRepositoryAsync<Domain.Entities.ClientPositionManager> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<List<ClientPositionManagerDTO>>> Handle(GetAllClientPositionManagerQuery request, CancellationToken cancellationToken)
        {
            var clientPositionManager = await _repositoryAsync.ListAsync();
            var clientPositionManagerDTO = _mapper.Map<List<ClientPositionManagerDTO>>(clientPositionManager);
            return new Response<List<ClientPositionManagerDTO>>(clientPositionManagerDTO);
        }
    }



}
