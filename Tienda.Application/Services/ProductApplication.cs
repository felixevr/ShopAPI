using AutoMapper;
using Tienda.Application.Commons.Bases;
using Tienda.Application.Dtos.Request;
using Tienda.Application.Dtos.Response;
using Tienda.Application.Interfaces;
using Tienda.Application.Validators;
using Tienda.Domain.Entities;
using Tienda.Infrastructure.Commons.Bases.Request;
using Tienda.Infrastructure.Commons.Bases.Response;
using Tienda.Infrastructure.Persistences.Interfaces;
using Tienda.Utilities.Static;

namespace Tienda.Application.Services
{
    public class ProductApplication : IProductApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ProductValidator _validationRules;
        public ProductApplication(IUnitOfWork unitOfWork, IMapper mapper, ProductValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
        }
        public async Task<BaseResponse<BaseEntityResponse<ProductResponseDto>>> ListProducts(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<ProductResponseDto>>();
            var products = await _unitOfWork.Product.ListProducts(filters);

            if (products is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<BaseEntityResponse<ProductResponseDto>>(products);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }

        public async Task<BaseResponse<IEnumerable<ProductSelectResponseDto>>> ListSelectProducts()
        {
            var response = new BaseResponse<IEnumerable<ProductSelectResponseDto>>();
            var products = await _unitOfWork.Product.GetAllAsync();

            if (products is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<IEnumerable<ProductSelectResponseDto>>(products);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }

        public async Task<BaseResponse<ProductSelectResponseDto>> ProductById(int productId)
        {
            var response = new BaseResponse<ProductSelectResponseDto>();
            var products = await _unitOfWork.Product.GetByIdAsync(productId);

            if (products is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<ProductSelectResponseDto>(products);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }

        public async Task<BaseResponse<bool>> RegisterProduct(ProductRequestDto requestDto)
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

            var product = _mapper.Map<Product>(requestDto);
            response.Data = await _unitOfWork.Product.RegisterAsync(product);

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

        public async Task<BaseResponse<bool>> EditProduct(int productId, ProductRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var productEdit = await ProductById(productId);

            if (productEdit.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            var product = _mapper.Map<Product>(requestDto);
            product.Id = productId;
            response.Data = await _unitOfWork.Product.EditAsync(product);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }

        public async Task<BaseResponse<bool>> RemoveProduct(int productId)
        {
            var response = new BaseResponse<bool>();
            var product = await ProductById(productId);

            if (product.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            response.Data = await _unitOfWork.Product.RemoveAsync(productId);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_DELETE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }
    }
}
