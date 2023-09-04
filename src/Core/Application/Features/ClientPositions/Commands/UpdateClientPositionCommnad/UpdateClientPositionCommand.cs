using Application.Exceptions;
using Application.Extensions;
using Application.Wrappers;
using Atos.Core.Abstractions.Publishers;
using Atos.Core.EventsDTO;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.ClientPositions.Commands.UpdateClientPositionCommnad
{
    public class UpdateClientPositionCommand : IRequest<Response<ClientPosition>>
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid PositionId { get; set; }
        public string PositionDescription { get; set; } = null!;
        public int CurrentStateId { get; set; }
        public string CurrentStateName { get; set; } = null!;
    }

    public class
        UpdateClientPositionCommnadHandler : IRequestHandler<UpdateClientPositionCommand, Response<ClientPosition>>
    {
        private readonly IClientPositionRepository _repositoryAsync;
        private readonly IPublisherCommands<ClientPositionUpdated> _publisherCommands;
        private readonly IMapper _mapper;

        public UpdateClientPositionCommnadHandler(IClientPositionRepository repositoryAsync,
            IPublisherCommands<ClientPositionUpdated> publisherCommands, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _publisherCommands = publisherCommands;
            _mapper = mapper;
        }

        public Task<Response<ClientPosition>> Handle(UpdateClientPositionCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            return HandleProcess(request, cancellationToken);
        }

        private async Task<Response<ClientPosition>> HandleProcess(UpdateClientPositionCommand request, CancellationToken cancellationToken)
        {
            var clientPosition = await _repositoryAsync.GetByIdAsync(request.Id, cancellationToken);

            if (clientPosition == null) throw new ApiExceptions($"register {request.Id} Not Found");

            clientPosition = _mapper.Map(request,clientPosition);

            await _repositoryAsync.UpdateAsync(clientPosition, cancellationToken);
            await _publisherCommands.PublishEntityMessage(request.ToClientPositionUpdated(), "clientPosition.updated",
                request.Id, cancellationToken);

            return new Response<ClientPosition>(clientPosition);
        }
    }
}