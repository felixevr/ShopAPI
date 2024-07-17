using AutoMapper;
using Tienda.Application.Dtos.Request;
using Tienda.Application.Dtos.Response;
using Tienda.Domain.Entities;
using Tienda.Infrastructure.Commons.Bases.Response;
using Tienda.Utilities.Static;

namespace Tienda.Application.Mappers
{
    public class ProductMapingsProfile : Profile
    {
        public ProductMapingsProfile()
        {
            CreateMap<Product, ProductResponseDto>()
                .ForMember(x => x.ProductId, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.StateUser, x => x.MapFrom(y => y.State.Equals((int) StateTypes.Active) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<BaseEntityResponse<Product>, BaseEntityResponse<ProductResponseDto>>()
                .ReverseMap();

            CreateMap<ProductRequestDto, Product>();

            CreateMap<ProductSelectResponseDto, Product>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.ProductId))
                .ReverseMap();
        }
    }
}
