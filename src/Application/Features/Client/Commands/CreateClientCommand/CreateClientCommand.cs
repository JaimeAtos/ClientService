using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Client.Commands.CreateClientCommand
{
    public class CreateClientCommand : IRequest<Response<Domain.Entities.Client>>
    {
        public string Name { get; set; } = null!;
        public Guid LocationId { get; set; }
        public string LocationName { get; set; } = null!;
        public int CountPositions { get; set; }
    }

    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Response<Domain.Entities.Client>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryAsync<Domain.Entities.Client> _repositoryAsync;
        public CreateClientCommandHandler(IRepositoryAsync<Domain.Entities.Client> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
    
        public  Task<Response<Domain.Entities.Client>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Create request is empty");
            }
            return HandleProcess(request, cancellationToken);
        }

        public async Task<Response<Domain.Entities.Client>> HandleProcess(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var client = _mapper.Map<Domain.Entities.Client>(request);
            await _repositoryAsync.AddAsync(client);
            return new Response<Domain.Entities.Client>(client);
        }
    }
}
