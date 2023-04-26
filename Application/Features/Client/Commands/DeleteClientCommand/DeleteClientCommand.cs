using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Client.Commands.DeleteClientCommand
{
    public class DeleteClientCommand : IRequest<Response<Domain.Entities.Client>>
    {
        public Guid Id { get; set; }
       
    }

    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Response<Domain.Entities.Client>>
    {
        private readonly IRepositoryAsync<Domain.Entities.Client> _repositoryAsync;

        public DeleteClientCommandHandler(IRepositoryAsync<Domain.Entities.Client> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public Task<Response<Domain.Entities.Client>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return HandleProcess(request, cancellationToken);
        }

        public async Task<Response<Domain.Entities.Client>> HandleProcess(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _repositoryAsync.GetByIdAsync(request.Id);
            if(client == null)
            {
                throw new ApiExceptions($"register {request.Id} Not Found");
            }
            else
            {
           
                client.State = false;
                await _repositoryAsync.UpdateAsync(client);
                return new Response<Domain.Entities.Client>(client);
            }
        }
    }
}
