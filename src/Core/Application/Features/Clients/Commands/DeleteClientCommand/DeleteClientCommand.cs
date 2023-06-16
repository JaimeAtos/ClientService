using Application.Exceptions;
using Application.Extensions;
using Application.Wrappers;
using Atos.Core.Abstractions.Publishers;
using Atos.Core.EventsDTO;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Clients.Commands.DeleteClientCommand
{
    public class DeleteClientCommand : IRequest<Response<Client>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Response<Client>>
    {
        private readonly IClientRepository _repositoryAsync;
        private readonly IPublisherCommands<ClientDeleted> _publisherCommands;

        public DeleteClientCommandHandler(IClientRepository repositoryAsync,
            IPublisherCommands<ClientDeleted> publisherCommands)
        {
            _repositoryAsync = repositoryAsync;
            _publisherCommands = publisherCommands;
        }

        public Task<Response<Client>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Delete request is empty");

            return HandleProcess(request, cancellationToken);
        }

        public Task<Response<Client>> HandleProcess(DeleteClientCommand request,
            CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request), "Create request is empty");
            return ProcessHandle(request, cancellationToken);
        }

        private async Task<Response<Client>> ProcessHandle(DeleteClientCommand request,
            CancellationToken cancellationToken)
        {
            var client = await _repositoryAsync.GetByIdAsync(request.Id, cancellationToken);
            if (client == null) throw new ApiExceptions(nameof(client), $"register {request.Id} Not Found");

            client.State = false;
            await _repositoryAsync.UpdateAsync(client, cancellationToken);
            await _publisherCommands.PublishEntityMessage(request.ToClientDeleted(), "client.deleted", request.Id,
                cancellationToken);

            return new Response<Client>(client);
        }
    }
}