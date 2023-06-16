using Application.DTOs;
using Application.Features.ClientPositions.Commands.CreateClientPositionCommand;
using Application.Features.ClientPositionsManager.Commands.CreateClientPositionManager;
using Application.Features.Clients.Commands.CreateClientCommand;
using Application.Features.LeaveRequests.Commands.CreateLeaveRequestCommand;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Commands
            CreateMap<CreateClientCommand, Client>();
            CreateMap<CreateClientPositionCommand, ClientPosition>();
            CreateMap<CreateClientPositionManagerCommand, ClientPositionManager>();
            CreateMap<CreateLeaveRequestCommand, LeaveRequest>();
            #endregion

            #region DTOs
            CreateMap<Client, ClientDTO>();
            CreateMap<ClientPosition, ClientPositionDTO>();
            CreateMap<ClientPositionManager, ClientPositionManagerDTO>();
            CreateMap<LeaveRequest, LeaveRequestDTO>();
            #endregion
        }
    }
}
