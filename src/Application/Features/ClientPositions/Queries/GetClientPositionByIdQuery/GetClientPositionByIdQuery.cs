using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ClientPositions.Queries.GetClientPositionByIdQuery
{
    public class GetClientPositionByIdQuery : IRequest<Response<ClientPositionDTO>>
    {
        public Guid Id { get; set; }
    }

    public class GetClientPositionByIdQueryHandler : IRequestHandler<GetClientPositionByIdQuery, Response<ClientPositionDTO>>
    {
        private readonly IRepositoryAsync<ClientPosition> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetClientPositionByIdQueryHandler(IRepositoryAsync<ClientPosition> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public Task<Response<ClientPositionDTO>> Handle(GetClientPositionByIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request),"Request mustn't be empty");
            return ProcessHandle(request, cancellationToken);
        }

        private async Task<Response<ClientPositionDTO>> ProcessHandle(GetClientPositionByIdQuery request, CancellationToken cancellationToken)
        {
            var clientPosition = await _repositoryAsync.GetByIdAsync(request.Id, cancellationToken);
            var clientPositionDto = _mapper.Map<ClientPositionDTO>(clientPosition);
            return new Response<ClientPositionDTO>(clientPositionDto);
        }
    }
}
