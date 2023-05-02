using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.Client.Queries.GetAllClients
{
    public class GetAllClientsQuery : IRequest<PagedResponse<List<ClientDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, PagedResponse<List<ClientDTO>>>
    {
        private readonly IRepositoryAsync<Domain.Entities.Client> _repositoryAsync;
        private readonly IMapper _mapper;
        public GetAllClientsQueryHandler(IRepositoryAsync<Domain.Entities.Client> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
        public Task<PagedResponse<List<ClientDTO>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return HandleProcess(request, cancellationToken);
        }

        public async Task<PagedResponse<List<ClientDTO>>> HandleProcess(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _repositoryAsync.ListAsync(new PagedClientsSpecification(request.PageSize, request.PageNumber));
            var clientsDTO = _mapper.Map<List<ClientDTO>>(clients);
            return new PagedResponse<List<ClientDTO>>(clientsDTO, request.PageNumber, request.PageSize) ;
        }
    }

}
