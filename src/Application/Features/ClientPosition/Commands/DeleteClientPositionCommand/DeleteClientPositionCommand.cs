using Application.Exceptions;
using Application.Extensions;
using Application.Interfaces;
using Application.Wrappers;
using Atos.Core.Abstractions.Publishers;
using Atos.Core.EventsDTO;
using MediatR;

namespace Application.Features.ClientPosition.Commands.DeleteClientPositionCommand
{
    public class DeleteClientPositionCommand : IRequest<Response<Domain.Entities.ClientPosition>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteClientPositionCommandHandler : IRequestHandler<DeleteClientPositionCommand,
        Response<Domain.Entities.ClientPosition>>
    {
        private readonly IRepositoryAsync<Domain.Entities.ClientPosition> _repositoryAsync;
        private readonly IPublisherCommands<ClientPositionDeleted> _publisherCommands;

        public DeleteClientPositionCommandHandler(IRepositoryAsync<Domain.Entities.ClientPosition> repositoryAsync,
            IPublisherCommands<ClientPositionDeleted> publisherCommands)
        {
            _repositoryAsync = repositoryAsync;
            _publisherCommands = publisherCommands;
        }

        public Task<Response<Domain.Entities.ClientPosition>> Handle(DeleteClientPositionCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "Delete request is empty");
            return HandleProcess(request, cancellationToken);
        }

        public async Task<Response<Domain.Entities.ClientPosition>> HandleProcess(DeleteClientPositionCommand request,
            CancellationToken cancellationToken)
        {
            var clientPosition = await _repositoryAsync.GetByIdAsync(request.Id);
            if (clientPosition == null)
            {
                throw new ApiExceptions($"register {request.Id} Not Found");
            }

            clientPosition.State = false;
            await _repositoryAsync.UpdateAsync(clientPosition, cancellationToken);
            await _publisherCommands.PublishEntityMessage(request.ToClientPositionDeleted(), "clientPosition.deleted",
                request.Id, cancellationToken);

            return new Response<Domain.Entities.ClientPosition>(clientPosition);
        }
    }
}