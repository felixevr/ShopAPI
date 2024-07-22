using AutoMapper;
using Tienda.Application.Dtos.Request;
using Tienda.Application.Dtos.Response;
using Tienda.Domain.Entities;
using Tienda.Infrastructure.Commons.Bases.Response;
using Tienda.Utilities.Static;

namespace Tienda.Application.Mappers
{
    public class CategoryMapingsProfile : Profile
    {
        public CategoryMapingsProfile()
        {
            CreateMap<Category, CategoryResponseDto>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(y => y.Id))
                .ForMember(dest => dest.StateCategory, opt => opt.MapFrom(src => src.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
               // .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products)) // No era necesaria para el mapeo
                .ReverseMap();
            // Personal Notes. Learning purpose only. Not intended for documentation.
            // x in the first parameter is destination member class (CategoryResponseDto)
            // x in the second parameter is a IMemberConfigurationExpression that specifies how Automap should make the map.
            // y is the source from mapping. Used when the name of the properties doesn't match.

            CreateMap<BaseEntityResponse<Category>, BaseEntityResponse<CategoryResponseDto>>()
                .ReverseMap();

            CreateMap<CategoryRequestDto, Category>();

            CreateMap<CategorySelectResponseDto, Category>() // No es neceario
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CategoryId)) // Inverted Map
                .ReverseMap();

            //CreateMap<Product, ProductResponseDto>() // No era necesaria para el mapeo
            //    .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
