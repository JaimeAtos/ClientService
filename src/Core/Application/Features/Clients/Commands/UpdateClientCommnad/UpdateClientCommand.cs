using Application.Exceptions;
using Application.Extensions;
using Application.Wrappers;
using Atos.Core.Abstractions.Publishers;
using Atos.Core.EventsDTO;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Clients.Commands.UpdateClientCommnad
{
    public class UpdateClientCommand : IRequest<Response<Client>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid LocationId { get; set; }
        public string LocationName { get; set; } = null!;
        public int CountPositions { get; set; }
    }

    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Response<Client>>
    {
        private readonly IClientRepository _repositoryAsync;
        private readonly IPublisherCommands<ClientUpdated> _publisherCommands;
        private readonly IMapper _mapper;

        public UpdateClientCommandHandler(IClientRepository repositoryAsync,
            IPublisherCommands<ClientUpdated> publisherCommands, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _publisherCommands = publisherCommands;
            _mapper = mapper;
        }

        public Task<Response<Client>> Handle(UpdateClientCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Update request is empty");

            return HandleProcess(request, cancellationToken);
        }

        private async Task<Response<Client>> HandleProcess(UpdateClientCommand request,
            CancellationToken cancellationToken)
        {
            var client = await _repositoryAsync.GetByIdAsync(request.Id, cancellationToken);
            if (client == null) throw new ApiExceptions($"register {request.Id} Not Found");

            client = _mapper.Map(request, client);
            

            await _repositoryAsync.UpdateAsync(client, cancellationToken);
            await _publisherCommands.PublishEntityMessage(request.ToClientUpdated(), "client.updated", request.Id,
                cancellationToken);
            return new Response<Client>(client);
        }
    }
}