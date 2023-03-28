using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;

namespace Application.Features.ClientPosition.Commands.UpdateClientPositionCommnad
{
    public class UpdateClientPositionCommnad : IRequest<Response<Domain.Entities.ClientPosition>>
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid PositionId { get; set; }
        public string PositionName { get; set; }
    }

    public class UpdateClientPositionCommnadHandler : IRequestHandler<UpdateClientPositionCommnad, Response<Domain.Entities.ClientPosition>>
    {
        private readonly IRepositoryAsync<Domain.Entities.ClientPosition> _repositoryAsync;

        public UpdateClientPositionCommnadHandler(IRepositoryAsync<Domain.Entities.ClientPosition> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
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

            clientPosition.PositionName = request.PositionName;

            await _repositoryAsync.UpdateAsync(clientPosition);
            return new Response<Domain.Entities.ClientPosition>(clientPosition);
        }
    }
}
