using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.ClientPosition.Queries.GetAllClientPositionsQuery
{
    public class GetAllClientPositionsQuery : IRequest<Response<List<ClientPositionDTO>>>
    {

    }

    public class GetAllClientPositionsQueryHandler : IRequestHandler<GetAllClientPositionsQuery, Response<List<ClientPositionDTO>>>
    {
        private readonly IRepositoryAsync<Domain.Entities.ClientPosition> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllClientPositionsQueryHandler(IRepositoryAsync<Domain.Entities.ClientPosition> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<List<ClientPositionDTO>>> Handle(GetAllClientPositionsQuery request, CancellationToken cancellationToken)
        {
            var clientPositions = await _repositoryAsync.ListAsync();
            var clientPositionsDTO = _mapper.Map<List<ClientPositionDTO>>(clientPositions);
            return new Response<List<ClientPositionDTO>>(clientPositionsDTO);
        }
    }
}
