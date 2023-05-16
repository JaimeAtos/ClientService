using Application.Features.ClientPosition.Commands.DeleteClientPositionCommand;
using Application.Features.ClientPosition.Commands.UpdateClientPositionCommnad;
using Atos.Core.EventsDTO;

namespace Application.Extensions;

public static class CommandClientPositionExtensions
{
    public static ClientPositionUpdated ToClientPositionUpdated(this UpdateClientPositionCommnad request)
    {
        return new ClientPositionUpdated
        {
            Id = request.Id,
            ClientId = request.ClientId,
            CurrentStateId = request.CurrentStateId,
            CurrentStateName = request.CurrentStateName,
            PositionId = request.PositionId,
            PositionDescription = request.PositionDescription
        };
    }

    public static ClientPositionDeleted ToClientPositionDeleted(this DeleteClientPositionCommand request)
    {
        return new ClientPositionDeleted
        {
            Id = request.Id
        };
    }
}