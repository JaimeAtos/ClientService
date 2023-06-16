using Application.Exceptions;
using Application.Wrappers;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.ClientPositionsManager.Commands.UpdateClientPositionManagerCommand
{
    public class UpdateClientPositionManagerCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public Guid ClientPositionId { get; set; }
        public Guid ResourceId { get; set; }
        public string Resource { get; set; }
    }

    public class
        UpdateClientPositionManagerCommandHandler : IRequestHandler<UpdateClientPositionManagerCommand, Response<Guid>>
    {
        private readonly IClientPositionManagerRepository _repositoryAsync;

        public UpdateClientPositionManagerCommandHandler(IClientPositionManagerRepository repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public Task<Response<Guid>> Handle(UpdateClientPositionManagerCommand request,
            CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request), "Create request is empty");
            return ProcessHandle(request, cancellationToken);
        }

        private async Task<Response<Guid>> ProcessHandle(UpdateClientPositionManagerCommand request,
            CancellationToken cancellationToken)
        {
            var clientResponseManager = await _repositoryAsync.GetByIdAsync(request.Id, cancellationToken);
            if (clientResponseManager == null) throw new ApiExceptions($"register {request.Id} Not Found");
            
            clientResponseManager.ClientPositionId = request.ClientPositionId;
            clientResponseManager.ResourceId = request.ResourceId;
            clientResponseManager.Resource = request.Resource;
            
            await _repositoryAsync.UpdateAsync(clientResponseManager, cancellationToken);
            return new Response<Guid>(clientResponseManager.Id);
        }
    }
}