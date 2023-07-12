using Application.Exceptions;
using Application.Extensions;
using Application.Wrappers;
using Ardalis.Specification;
using Atos.Core.Abstractions.Publishers;
using Atos.Core.EventsDTO;
using Domain.Entities;
using MediatR;

namespace Application.Features.ClientPositions.Commands.UpdateClientPositionCommnad
{
    public class UpdateClientPositionCommnad : IRequest<Response<ClientPosition>>
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid PositionId { get; set; }
        public string PositionDescription { get; set; } = null!;
        public int CurrentStateId { get; set; }
        public string CurrentStateName { get; set; } = null!;
    }

    public class
        UpdateClientPositionCommnadHandler : IRequestHandler<UpdateClientPositionCommnad, Response<ClientPosition>>
    {
        private readonly IRepositoryBase<ClientPosition> _repositoryAsync;
        private readonly IPublisherCommands<ClientPositionUpdated> _publisherCommands;

        public UpdateClientPositionCommnadHandler(IRepositoryBase<ClientPosition> repositoryAsync,
            IPublisherCommands<ClientPositionUpdated> publisherCommands)
        {
            _repositoryAsync = repositoryAsync;
            _publisherCommands = publisherCommands;
        }

        public Task<Response<ClientPosition>> Handle(UpdateClientPositionCommnad request,
            CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            return HandleProcess(request, cancellationToken);
        }

        private async Task<Response<ClientPosition>> HandleProcess(UpdateClientPositionCommnad request, CancellationToken cancellationToken)
        {
            var clientPosition = await _repositoryAsync.GetByIdAsync(request.Id, cancellationToken);

            if (clientPosition == null) throw new ApiExceptions($"register {request.Id} Not Found");

            clientPosition.PositionDescription = request.PositionDescription;

            await _repositoryAsync.UpdateAsync(clientPosition, cancellationToken);
            await _publisherCommands.PublishEntityMessage(request.ToClientPositionUpdated(), "clientPosition.updated",
                request.Id, cancellationToken);

            return new Response<ClientPosition>(clientPosition);
        }
    }
}