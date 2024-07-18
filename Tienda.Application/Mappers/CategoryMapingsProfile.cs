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
                .ForMember(x => x.CategoryId, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.StateCategory, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
                .ReverseMap();
            // Personal Notes. Learning purpose only. Not intended for documentation.
            // x in the first parameter is destination member class (CategoryResponseDto)
            // x in the second parameter is a IMemberConfigurationExpression that specifies how Automap should make the map.
            // y is the source from mapping. Used when the name of the properties doesn't match.

            CreateMap<BaseEntityResponse<Category>, BaseEntityResponse<CategoryResponseDto>>()
                .ReverseMap();

            CreateMap<CategoryRequestDto, Category>();

            CreateMap<CategorySelectResponseDto, Category>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.CategoryId)) // Inverted Map
                .ReverseMap();
            
            CreateMap<Product, ProductResponseDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
