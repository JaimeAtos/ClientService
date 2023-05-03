using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.ClientPosition.Queries.GetClientPositionByIdQuery
{
    public class GetClientPositionByIdQuery : IRequest<Response<ClientPositionDTO>>
    {
        public Guid Id { get; set; }
    }

    public class GetClientPositionByIdQueryHandler : IRequestHandler<GetClientPositionByIdQuery, Response<ClientPositionDTO>>
    {
        private readonly IRepositoryAsync<Domain.Entities.ClientPosition> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetClientPositionByIdQueryHandler(IRepositoryAsync<Domain.Entities.ClientPosition> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<ClientPositionDTO>> Handle(GetClientPositionByIdQuery request, CancellationToken cancellationToken)
        {
            var clientPosition = await _repositoryAsync.GetByIdAsync(request.Id);
            var clientPositionDTO = _mapper.Map<ClientPositionDTO>(clientPosition);
            return new Response<ClientPositionDTO>(clientPositionDTO);
        }
    }
}
