using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.ClientPositionsManager.Commands.DeleteClientPositionManagerCommand
{
    public class DeleteClientPositionManagerCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
    }


    public class
        DeleteClientPositionManagerCommandHandler : IRequestHandler<DeleteClientPositionManagerCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<ClientPositionManager> _repositoryAsync;

        public DeleteClientPositionManagerCommandHandler(IRepositoryAsync<ClientPositionManager> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public Task<Response<Guid>> Handle(DeleteClientPositionManagerCommand request,
            CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request), "Create request is empty");
            return ProcessHandle(request, cancellationToken);
        }

        private async Task<Response<Guid>> ProcessHandle(DeleteClientPositionManagerCommand request,
            CancellationToken cancellationToken)
        {
            var clientPositionManager = await _repositoryAsync.GetByIdAsync(request.Id, cancellationToken);
            if (clientPositionManager == null) throw new ApiExceptions($"register {request.Id} Not Found");

            clientPositionManager.State = false;
            await _repositoryAsync.UpdateAsync(clientPositionManager, cancellationToken);
            return new Response<Guid>(clientPositionManager.Id);
        }
    }
}