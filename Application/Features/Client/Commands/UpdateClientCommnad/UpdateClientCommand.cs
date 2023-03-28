using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Client.Commands.UpdateClientCommnad
{
    public class UpdateClientCommand : IRequest<Response<Domain.Entities.Client>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid LocationId { get; set; }
        public int CountPositions { get; set; }
    }

    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Response<Domain.Entities.Client>>
    {
        private readonly IRepositoryAsync<Domain.Entities.Client> _repositoryAsync;

        public UpdateClientCommandHandler(IRepositoryAsync<Domain.Entities.Client> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public Task<Response<Domain.Entities.Client>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return HandleProcess(request, cancellationToken);
        }

        public async Task<Response<Domain.Entities.Client>> HandleProcess(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _repositoryAsync.GetByIdAsync(request.Id);
            if (client == null)
            {
                throw new ApiExceptions($"register {request.Id} Not Found");
            }
            else
            {
                client.Name = request.Name;
                client.CountPositions = request.CountPositions;
                
                await _repositoryAsync.UpdateAsync(client);
                return new Response<Domain.Entities.Client>(client);
            }
        }
    }
}
