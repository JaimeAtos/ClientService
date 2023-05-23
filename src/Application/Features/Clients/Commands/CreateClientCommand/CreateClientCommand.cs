using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Clients.Commands.CreateClientCommand
{
    public class CreateClientCommand : IRequest<Response<Client>>
    {
        public string Name { get; set; } = null!;
        public Guid LocationId { get; set; }
        public string LocationName { get; set; } = null!;
        public int CountPositions { get; set; }
    }

    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Response<Client>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryAsync<Client> _repositoryAsync;

        public CreateClientCommandHandler(IRepositoryAsync<Client> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public Task<Response<Client>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Create request is empty");

            return HandleProcess(request, cancellationToken);
        }

        public async Task<Response<Client>> HandleProcess(CreateClientCommand request,
            CancellationToken cancellationToken)
        {
            var client = _mapper.Map<Client>(request);
            await _repositoryAsync.AddAsync(client, cancellationToken);
            return new Response<Client>(client);
        }
    }
}