using Application.Exceptions;
using Application.Extensions;
using Application.Interfaces;
using Application.Wrappers;
using Atos.Core.Abstractions.Publishers;
using Atos.Core.EventsDTO;
using MediatR;

namespace Application.Features.ClientPosition.Commands.UpdateClientPositionCommnad
{
    public class UpdateClientPositionCommnad : IRequest<Response<Domain.Entities.ClientPosition>>
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid PositionId { get; set; }
        public string PositionDescription { get; set; } = null!;
        public Guid CurrentStateId { get; set; }
        public string CurrentStateName { get; set; } = null!;
    }

    public class UpdateClientPositionCommnadHandler : IRequestHandler<UpdateClientPositionCommnad, Response<Domain.Entities.ClientPosition>>
    {
        private readonly IRepositoryAsync<Domain.Entities.ClientPosition> _repositoryAsync;
        private readonly IPublisherCommands<ClientPositionUpdated> _publisherCommands;

        public UpdateClientPositionCommnadHandler(IRepositoryAsync<Domain.Entities.ClientPosition> repositoryAsync, IPublisherCommands<ClientPositionUpdated> publisherCommands)
        {
            _repositoryAsync = repositoryAsync;
            _publisherCommands = publisherCommands;
        }

        public async Task<Response<Domain.Entities.ClientPosition>> Handle(UpdateClientPositionCommnad request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var clientPosition = await _repositoryAsync.GetByIdAsync(request.Id);
            if (clientPosition == null)
            {
                throw new ApiExceptions($"register {request.Id} Not Found");
            }

            clientPosition.PositionDescription = request.PositionDescription;

            await _repositoryAsync.UpdateAsync(clientPosition, cancellationToken);
            await _publisherCommands.PublishEntityMessage(request.ToClientPositionUpdated(), "clientPosition.updated",
                request.Id, cancellationToken);

            return new Response<Domain.Entities.ClientPosition>(clientPosition);
        }
    }
}
