using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.ClientPositionManager.Commands.UpdateClientPositionManagerCommand
{
    public class UpdateClientPositionManagerCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public Guid ClientPositionId { get; set; }
        public Guid ResourceId { get; set; }
        public string Resource { get; set; }
    }
    
    public class UpdateClientPositionManagerCommandHandler : IRequestHandler<UpdateClientPositionManagerCommand, Response<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryAsync<Domain.Entities.ClientPositionManager> _repositoryAsync;

        public UpdateClientPositionManagerCommandHandler(IMapper mapper, IRepositoryAsync<Domain.Entities.ClientPositionManager> repositoryAsync)
        {
            _mapper = mapper;
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<Guid>> Handle(UpdateClientPositionManagerCommand request, CancellationToken cancellationToken)
        {
            var clientResponseManager = await _repositoryAsync.GetByIdAsync(request.Id);
            if (clientResponseManager == null)
            {
                throw new ApiExceptions($"register {request.Id} Not Found");
            }
            else
            {
                clientResponseManager.ClientPositionId = request.ClientPositionId;
                clientResponseManager.ResourceId = request.ResourceId;
                clientResponseManager.Resource = request.Resource;
                await _repositoryAsync.UpdateAsync(clientResponseManager);
                return new Response<Guid>(clientResponseManager.Id);
            }
        }
    }
}
