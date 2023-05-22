using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Clients.Queries.GetClientById
{
    public class GetClientByIdQuery : IRequest<Response<ClientDTO>>
    {
        public Guid Id { get; set; }
    }

    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Response<ClientDTO>>
    {
        private readonly IRepositoryAsync<Client> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetClientByIdQueryHandler(IRepositoryAsync<Client> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
        
        public async Task<Response<ClientDTO>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Get by id request is empty");

            var client = await _repositoryAsync.GetByIdAsync(request.Id, cancellationToken);
            
            if (client == null) throw new ApiExceptions($"Client with {request.Id} not found");
            
            var clientDto = _mapper.Map<ClientDTO>(client);
            return new Response<ClientDTO>(clientDto);
        }


    }
}
