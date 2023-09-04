using Application.DTOs;
using Application.Features.ClientPositions.Commands.CreateClientPositionCommand;
using Application.Features.ClientPositions.Commands.UpdateClientPositionCommnad;
using Application.Features.ClientPositionsManager.Commands.CreateClientPositionManager;
using Application.Features.ClientPositionsManager.Commands.UpdateClientPositionManagerCommand;
using Application.Features.Clients.Commands.CreateClientCommand;
using Application.Features.Clients.Commands.UpdateClientCommnad;
using Application.Features.LeaveRequests.Commands.CreateLeaveRequestCommand;
using Application.Features.LeaveRequests.Commands.UpdateLeaveRquestCommand;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Create
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
            
            #region Update
            CreateMap<UpdateClientCommand, Client>();
            CreateMap<UpdateClientPositionCommand, ClientPosition>();
            CreateMap<UpdateClientPositionManagerCommand, ClientPositionManager>();
            CreateMap<UpdateLeaveRequestCommand, LeaveRequest>();
            #endregion
        }
    }
}
