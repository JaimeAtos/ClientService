using Application.Exceptions;
using Application.Extensions;
using Application.Wrappers;
using Ardalis.Specification;
using Atos.Core.Abstractions.Publishers;
using Atos.Core.EventsDTO;
using Domain.Entities;
using MediatR;

namespace Application.Features.ClientPositions.Commands.DeleteClientPositionCommand
{
    public class DeleteClientPositionCommand : IRequest<Response<ClientPosition>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteClientPositionCommandHandler : IRequestHandler<DeleteClientPositionCommand,
        Response<ClientPosition>>
    {
        private readonly IRepositoryBase<ClientPosition> _repositoryAsync;
        private readonly IPublisherCommands<ClientPositionDeleted> _publisherCommands;

        public DeleteClientPositionCommandHandler(IRepositoryBase<ClientPosition> repositoryAsync,
            IPublisherCommands<ClientPositionDeleted> publisherCommands)
        {
            _repositoryAsync = repositoryAsync;
            _publisherCommands = publisherCommands;
        }

        public Task<Response<ClientPosition>> Handle(DeleteClientPositionCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Delete request is empty");
            return HandleProcess(request, cancellationToken);
        }

        public async Task<Response<ClientPosition>> HandleProcess(DeleteClientPositionCommand request,
            CancellationToken cancellationToken)
        {
            var clientPosition = await _repositoryAsync.GetByIdAsync(request.Id);
            if (clientPosition == null) throw new ApiExceptions($"register {request.Id} Not Found");

            clientPosition.State = false;

            await _repositoryAsync.UpdateAsync(clientPosition, cancellationToken);

            await _publisherCommands.PublishEntityMessage(request.ToClientPositionDeleted(), "clientPosition.deleted",
                request.Id, cancellationToken);

            return new Response<ClientPosition>(clientPosition);
        }
    }
}