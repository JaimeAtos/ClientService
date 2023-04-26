using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Client.Commands.DeleteClientWithReasonCommandd
{
    public class DeleteClientWithReasonCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public string LeaveReason { get; set; }
    }

    public class DeleteClientWithReasonCommandHandler : IRequestHandler<DeleteClientWithReasonCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Domain.Entities.Client> _repositoryAsync;

        public DeleteClientWithReasonCommandHandler(IRepositoryAsync<Domain.Entities.Client> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }
        public async Task<Response<Guid>> Handle(DeleteClientWithReasonCommand request, CancellationToken cancellationToken)
        {
            var client = await _repositoryAsync.GetByIdAsync(request.Id);
            if (client == null)
            {
                throw new ApiExceptions($"{request.Id} not found");
            }
            else
            {
                client.State = false;
                await _repositoryAsync.UpdateAsync(client);
                return new Response<Guid>(client.Id);
            }
        }
    }
}
