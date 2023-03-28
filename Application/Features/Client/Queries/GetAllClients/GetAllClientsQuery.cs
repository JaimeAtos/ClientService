using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.Client.Queries.GetAllClients
{
    public class GetAllClientsQuery : IRequest<Response<List<ClientDTO>>>
    {
    }

    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, Response<List<ClientDTO>>>
    {
        private readonly IRepositoryAsync<Domain.Entities.Client> _repositoryAsync;
        private readonly IMapper _mapper;
        public GetAllClientsQueryHandler(IRepositoryAsync<Domain.Entities.Client> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
        public Task<Response<List<ClientDTO>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return HandleProcess(request, cancellationToken);
        }

        public async Task<Response<List<ClientDTO>>> HandleProcess(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _repositoryAsync.ListAsync();
            var clientsDTO = _mapper.Map<List<ClientDTO>>(clients);
            return new Response<List<ClientDTO>>(clientsDTO) ;
        }
    }

}
