using AutoMapper;
using Tienda.Application.Commons.Bases;
using Tienda.Application.Dtos.Request;
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
        // Este es un buen lugar para inyectar las credenciales de Login del usuario (UserId, Token, etc) -- TABLA LOGIN
        private readonly int _userId = 5;
        public CartApplication(IUnitOfWork unitOfWork, IMapper mapper, CartValidator validationRules/*, int userId*/)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
            //_userId = userId;
        }

        public async Task<BaseResponse<BaseEntityResponse<CartResponseDto>>> ListCartsWithProducts(int userId)
        {
            var response = new BaseResponse<BaseEntityResponse<CartResponseDto>>();
            var carts = await _unitOfWork.Cart.ListCartsWithProducts(userId);

            if (carts is not null && carts.TotalRecords > 0)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<BaseEntityResponse<CartResponseDto>>(carts);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                var requestDto = new CartRequestDto();
                requestDto.UserId = userId;
                requestDto.State = 1;

                var register = await RegisterCart(requestDto);

                if (register.IsSuccess)
                {
                    response.IsSuccess = register.IsSuccess;
                    response.Data = _mapper.Map<BaseEntityResponse<CartResponseDto>>(carts);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            return response;

        }

        public async Task<BaseResponse<bool>> RegisterCart(CartRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var validationResult = await _validationRules.ValidateAsync(requestDto);

            if (!validationResult.IsValid)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_VALIDATE;
                response.Errors = validationResult.Errors;
                return response;
            }

            var cart = _mapper.Map<Cart>(requestDto);
            response.Data = await _unitOfWork.Cart.RegisterAsync(cart);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }

        //AQUÍ PUEDO USAR EL "CartSelectResponseDto". Solo tiene sentido si deseo regresar dos datos en vez de todo el response
        public async Task<BaseResponse<CartResponseDto>> GetCartById( int cartId) 
        {
            var response = new BaseResponse<CartResponseDto>();
            var cart = await _unitOfWork.Cart.GetByIdAsync(cartId);

            if (cart is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<CartResponseDto>(cart);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }

            return response;
        }

        // Método NO Recomendado hasta tener implementado los datos del usuario por DI
        public async Task<BaseResponse<BaseEntityResponse<CartResponseDto>>> GetCartByIdWithProducts(int cartId) 
        {
            var response = new BaseResponse<BaseEntityResponse<CartResponseDto>>();
            var cart = await _unitOfWork.Cart.GetCartByIdWithProducts(cartId);

            if (cart is not null && cart.TotalRecords >0)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<BaseEntityResponse<CartResponseDto>>(cart);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                var requestDto = new CartRequestDto();
                requestDto.UserId = _userId;
                requestDto.State = 1;

                var register = await RegisterCart(requestDto);

                if (register.IsSuccess)
                {
                    response.IsSuccess = register.IsSuccess;
                    response.Data = _mapper.Map<BaseEntityResponse<CartResponseDto>>(cart);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }

            return response;
        }
    }
}
