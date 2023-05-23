using Application.Features.Clients.Commands.DeleteClientCommand;
using Application.Features.Clients.Commands.UpdateClientCommnad;
using Atos.Core.EventsDTO;

namespace Application.Extensions;

public static class CommandClientExtensions
{
    public static ClientUpdated ToClientUpdated(this UpdateClientCommand request)
    {
        return new ClientUpdated
        {
            Id = request.Id,
            Name = request.Name,
            LocationId = request.LocationId,
            LocationName = request.LocationName,
            CountPositions = request.CountPositions
        };
    }
    
    public static ClientDeleted ToClientDeleted(this DeleteClientCommand request)
    {
        return new ClientDeleted
        {
            Id = request.Id
        };
    }
}