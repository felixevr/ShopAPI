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
    public class UserApplication : IUserApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserValidator _validationRules;
        public UserApplication(IUnitOfWork unitOfWork, IMapper mapper, UserValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
        }
        public async Task<BaseResponse<BaseEntityResponse<UserResponseDto>>> ListUsers(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<UserResponseDto>>();
            var users = await _unitOfWork.User.ListUsers(filters);

            if (users is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<BaseEntityResponse<UserResponseDto>>(users);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }

        public async Task<BaseResponse<IEnumerable<UserSelectResponseDto>>> ListSelectUsers()
        {
            var response = new BaseResponse<IEnumerable<UserSelectResponseDto>>();
            var users = await _unitOfWork.User.GetAllAsync();

            if (users is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<IEnumerable<UserSelectResponseDto>>(users);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }

        public async Task<BaseResponse<UserSelectResponseDto>> UserById(int userId)
        {
            var response = new BaseResponse<UserSelectResponseDto>();
            var users = await _unitOfWork.User.GetByIdAsync(userId);

            if (users is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<UserSelectResponseDto>(users);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }

        public async Task<BaseResponse<bool>> RegisterUser(UserRequestDto requestDto)
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

            var user = _mapper.Map<User>(requestDto);
            response.Data = await _unitOfWork.User.RegisterAsync(user);

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

        public async Task<BaseResponse<bool>> EditUser(int userId, UserRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var userEdit = await UserById(userId);

            if (userEdit.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            var user = _mapper.Map<User>(requestDto);
            user.Id = userId;
            response.Data = await _unitOfWork.User.EditAsync(user);

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

        public async Task<BaseResponse<bool>> RemoveUser(int userId)
        {
            var response = new BaseResponse<bool>();
            var user = await UserById(userId);

            if (user.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            response.Data = await _unitOfWork.User.RemoveAsync(userId);

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