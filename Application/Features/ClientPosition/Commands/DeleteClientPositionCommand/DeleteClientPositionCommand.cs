using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ClientPosition.Commands.DeleteClientPositionCommand
{
    public class DeleteClientPositionCommand : IRequest<Response<Domain.Entities.ClientPosition>>
    {
        public Guid Id { get; set; }
        public bool State { get; set; }
    }

    public class DeleteClientPositionCommandHandler : IRequestHandler<DeleteClientPositionCommand, Response<Domain.Entities.ClientPosition>>
    {
        private readonly IRepositoryAsync<Domain.Entities.ClientPosition> _repositoryAsync;

        public DeleteClientPositionCommandHandler(IRepositoryAsync<Domain.Entities.ClientPosition> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }
        public Task<Response<Domain.Entities.ClientPosition>> Handle(DeleteClientPositionCommand request, CancellationToken cancellationToken)
        {
            if(request == null)
                throw new ArgumentNullException(nameof(request));
            return HandleProcess(request, cancellationToken);
        }

        public async Task<Response<Domain.Entities.ClientPosition>> HandleProcess(DeleteClientPositionCommand request, CancellationToken cancellationToken)
        {
            var clientPosition = await _repositoryAsync.GetByIdAsync(request.Id);
            if(clientPosition == null)
            {
                throw new ApiExceptions($"register {request.Id} Not Found");
            }
            else
            {
                clientPosition.State = request.State;
                await _repositoryAsync.UpdateAsync(clientPosition);
                return new Response<Domain.Entities.ClientPosition>(clientPosition);
            }
        }
    }
}
