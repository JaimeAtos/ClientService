using Application.Exceptions;
using Application.Extensions;
using Application.Wrappers;
using Ardalis.Specification;
using Atos.Core.Abstractions.Publishers;
using Atos.Core.EventsDTO;
using Domain.Entities;
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
        private readonly IRepositoryBase<Client> _repositoryAsync;
        private readonly IPublisherCommands<ClientUpdated> _publisherCommands;

        public UpdateClientCommandHandler(IRepositoryBase<Client> repositoryAsync,
            IPublisherCommands<ClientUpdated> publisherCommands)
        {
            _repositoryAsync = repositoryAsync;
            _publisherCommands = publisherCommands;
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

            client.Name = request.Name;
            client.CountPositions = request.CountPositions;

            await _repositoryAsync.UpdateAsync(client, cancellationToken);
            await _publisherCommands.PublishEntityMessage(request.ToClientUpdated(), "client.updated", request.Id,
                cancellationToken);
            return new Response<Client>(client);
        }
    }
}