using Application.DTOs;
using Application.Features.Client.Commands.CreateClientCommand;
using Application.Features.ClientPosition.Commands.CreateClientPositionCommand;
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
            #endregion

            #region DTOs
            CreateMap<Client, ClientDTO>();
            CreateMap<ClientPosition, ClientPositionDTO>();
            #endregion
        }
    }
}
