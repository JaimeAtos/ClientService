using Application.DTOs;
using Application.Features.Client.Commands.CreateClientCommand;
using Application.Features.ClientPosition.Commands.CreateClientPositionCommand;
using Application.Features.ClientPositionManager.Commands.CreateClientPositionManager;
using Application.Features.LeaveRequest.Commands.CreateLeaveRequestCommand;
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
