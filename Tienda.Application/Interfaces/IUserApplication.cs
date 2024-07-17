using Tienda.Application.Commons.Bases;
using Tienda.Application.Dtos.Request;
using Tienda.Application.Dtos.Response;
using Tienda.Infrastructure.Commons.Bases.Request;
using Tienda.Infrastructure.Commons.Bases.Response;

namespace Tienda.Application.Interfaces
{
    public interface IUserApplication
    {
        Task<BaseResponse<BaseEntityResponse<UserResponseDto>>> ListUsers(BaseFiltersRequest filters);
        Task<BaseResponse<IEnumerable<UserSelectResponseDto>>> ListSelectUsers();
        Task<BaseResponse<UserSelectResponseDto>> UserById(int UserId);
        Task<BaseResponse<bool>> RegisterUser(UserRequestDto requestDto);
        Task<BaseResponse<bool>> EditUser(int UserId, UserRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveUser(int UserId);
    }
}
