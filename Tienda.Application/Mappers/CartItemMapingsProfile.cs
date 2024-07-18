using AutoMapper;
using Tienda.Application.Dtos.Request;
using Tienda.Application.Dtos.Response;
using Tienda.Domain.Entities;
using Tienda.Infrastructure.Commons.Bases.Response;
using Tienda.Utilities.Static;

namespace Tienda.Application.Mappers
{
    public class CartItemMapingsProfile : Profile
    {
        public CartItemMapingsProfile()
        {
            CreateMap<CartItem, CartItemResponseDto>()
                .ForMember(x => x.CartItemId, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.StateCartItem, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<BaseEntityResponse<Cart>, BaseEntityResponse<CartResponseDto>>()
                .ReverseMap();

            CreateMap<CartRequestDto, Cart>();

            //CreateMap<CartSelectResponseDto, Cart>()
            //    .ForMember(x => x.Id, x => x.MapFrom(y => y.CartId))
            //    .ReverseMap();
        }
    }
}
