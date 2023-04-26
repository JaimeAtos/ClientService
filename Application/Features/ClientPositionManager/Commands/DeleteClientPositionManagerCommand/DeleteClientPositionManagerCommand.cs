using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ClientPositionManager.Commands.DeleteClientPositionCommand
{
    public class DeleteClientPositionManagerCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
    }


    public class DeleteClientPositionManagerCommandHandler : IRequestHandler<DeleteClientPositionManagerCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Domain.Entities.ClientPositionManager> _repositoryAsync;

        public DeleteClientPositionManagerCommandHandler(IRepositoryAsync<Domain.Entities.ClientPositionManager> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<Guid>> Handle(DeleteClientPositionManagerCommand request, CancellationToken cancellationToken)
        {
            var clientPositionManager = await _repositoryAsync.GetByIdAsync(request.Id);
            if (clientPositionManager == null)
            {
                throw new ApiExceptions($"register {request.Id} Not Found");
            }
            else
            {
                clientPositionManager.State = false;
                await _repositoryAsync.UpdateAsync(clientPositionManager);
                return new Response<Guid>(clientPositionManager.Id);
            }
        }
    }

}
