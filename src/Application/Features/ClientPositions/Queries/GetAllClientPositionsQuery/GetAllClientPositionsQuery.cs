using Application.DTOs;
using Application.Interfaces;
using Application.Specification;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ClientPositions.Queries.GetAllClientPositionsQuery
{
    public class GetAllClientPositionsQuery : IRequest<PagedResponse<List<ClientPositionDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? PositionDescription { get; set; }
        public string? CurrentStateName { get; set; }
        public bool State { get; set; }
    }

    public class
        GetAllClientPositionsQueryHandler : IRequestHandler<GetAllClientPositionsQuery,
            PagedResponse<List<ClientPositionDTO>>>
    {
        private readonly IRepositoryAsync<ClientPosition> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllClientPositionsQueryHandler(IRepositoryAsync<ClientPosition> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public Task<PagedResponse<List<ClientPositionDTO>>> Handle(GetAllClientPositionsQuery request,
            CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            return HandleProcess(request, cancellationToken);

        }

        private async Task<PagedResponse<List<ClientPositionDTO>>> HandleProcess(GetAllClientPositionsQuery request, CancellationToken cancellationToken)
        {
            var pagination = new PagedClientPositionSpecification(request);
            var clientPositions = await _repositoryAsync.ListAsync(pagination, cancellationToken);
            var clientPositionsDto = _mapper.Map<List<ClientPositionDTO>>(clientPositions);
            return new PagedResponse<List<ClientPositionDTO>>(clientPositionsDto, request.PageNumber, request.PageSize);
        }
    }
}