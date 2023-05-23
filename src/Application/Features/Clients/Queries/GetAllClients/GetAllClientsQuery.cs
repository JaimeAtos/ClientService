using Application.DTOs;
using Application.Interfaces;
using Application.Specification;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Clients.Queries.GetAllClients
{
    public class GetAllClientsQuery : IRequest<PagedResponse<List<ClientDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Name { get; set; }
        public Guid? LocationId { get; set; }
        public string? LocationName { get; set; }
        public int? CountPositions { get; set; }
        public bool State { get; set; }
    }

    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, PagedResponse<List<ClientDTO>>>
    {
        private readonly IRepositoryAsync<Client> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllClientsQueryHandler(IRepositoryAsync<Client> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public Task<PagedResponse<List<ClientDTO>>> Handle(GetAllClientsQuery request,
            CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request), "Get all request is empty");

            return HandleProcess(request, cancellationToken);
        }

        public async Task<PagedResponse<List<ClientDTO>>> HandleProcess(GetAllClientsQuery request,
            CancellationToken cancellationToken)
        {
            var pagination = new PagedClientSpecification(request);
            var clients = await _repositoryAsync.ListAsync(pagination, cancellationToken);
            var clientsDto = _mapper.Map<List<ClientDTO>>(clients);
            return new PagedResponse<List<ClientDTO>>(clientsDto, request.PageNumber, request.PageSize);
        }
    }
}