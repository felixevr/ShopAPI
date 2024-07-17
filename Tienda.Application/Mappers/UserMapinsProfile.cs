using AutoMapper;
using Tienda.Application.Dtos.Request;
using Tienda.Application.Dtos.Response;
using Tienda.Domain.Entities;
using Tienda.Infrastructure.Commons.Bases.Response;
using Tienda.Utilities.Static;

namespace Tienda.Application.Mappers
{
    public class UserMapinsProfile : Profile
    {
        public UserMapinsProfile()
        {
            CreateMap<User, UserResponseDto>()
                .ForMember(x => x.UserId, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.StateUser, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<BaseEntityResponse<User>, BaseEntityResponse<UserResponseDto>>()
                .ReverseMap();

            CreateMap<UserRequestDto, User>();

            CreateMap<UserSelectResponseDto, User>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.UserId))
                .ReverseMap();
        }
    }
}
