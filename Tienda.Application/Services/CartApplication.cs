using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tienda.Application.Commons.Bases;
using Tienda.Application.Dtos.Response;
using Tienda.Application.Interfaces;
using Tienda.Application.Validators;
using Tienda.Domain.Entities;
using Tienda.Infrastructure.Commons.Bases.Response;
using Tienda.Infrastructure.Persistences.Interfaces;
using Tienda.Utilities.Static;

namespace Tienda.Application.Services
{
    public class CartApplication : ICartApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CartValidator _validationRules; // OJO con esta validación
        public CartApplication(IUnitOfWork unitOfWork, IMapper mapper, CartValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
        }

        public async Task<BaseResponse<BaseEntityResponse<CartResponseDto>>> ListCartsWithProducts()
        {
            var response = new BaseResponse<BaseEntityResponse<CartResponseDto>>();
            var carts = await _unitOfWork.Cart.ListCartsWithProducts();
            
            if (carts is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<BaseEntityResponse<CartResponseDto>>(carts);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        
        }
    }
}
