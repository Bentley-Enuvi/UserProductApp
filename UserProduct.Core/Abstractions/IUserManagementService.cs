using UserProduct.Core.DTOs;

namespace UserProduct.Core.Abstractions
{
    public interface IUserManagementService
    {
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto> GetUserById(string userId);
        Task<object> DeleteUser(string Id);
        Task<UpdateUserRequestDto> UpdateUser(string id, UpdateUserRequestDto appUser);
        Task<IEnumerable<GetAllProductDto>> GetProductsPurchasedByUser(string userId);
    }
}

