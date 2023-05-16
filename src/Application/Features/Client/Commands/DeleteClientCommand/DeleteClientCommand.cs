using Application.Exceptions;
using Application.Extensions;
using Application.Interfaces;
using Application.Wrappers;
using Atos.Core.Abstractions.Publishers;
using Atos.Core.EventsDTO;
using MediatR;

namespace Application.Features.Client.Commands.DeleteClientCommand
{
    public class DeleteClientCommand : IRequest<Response<Domain.Entities.Client>>
    {
        public Guid Id { get; set; }
       
    }

    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Response<Domain.Entities.Client>>
    {
        private readonly IRepositoryAsync<Domain.Entities.Client> _repositoryAsync;
        private readonly IPublisherCommands<ClientDeleted> _publisherCommands;

        public DeleteClientCommandHandler(IRepositoryAsync<Domain.Entities.Client> repositoryAsync, IPublisherCommands<ClientDeleted> publisherCommands)
        {
            _repositoryAsync = repositoryAsync;
            _publisherCommands = publisherCommands;
        }

        public Task<Response<Domain.Entities.Client>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request), "Delete request is empty");
            }

            return HandleProcess(request, cancellationToken);
        }

        public async Task<Response<Domain.Entities.Client>> HandleProcess(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _repositoryAsync.GetByIdAsync(request.Id);
            if(client == null)
            {
                throw new ApiExceptions($"register {request.Id} Not Found");
            }

            client.State = false;
            await _repositoryAsync.UpdateAsync(client, cancellationToken);
            await _publisherCommands.PublishEntityMessage(request.ToClientDeleted(), "client.deleted", request.Id,
                cancellationToken);
            
            return new Response<Domain.Entities.Client>(client);
        }
    }
}
