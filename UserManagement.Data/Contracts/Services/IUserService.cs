using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Data.Dto.User;
using UserManagement.Domain.Model.User;

namespace UserManagement.Domain.Contracts.Services
{
    internal interface IUserService
    {
        Task<UserDto> AddUser(AddUserModel addUser);
        Task<UserDto> ChangePassword(ChangePasswordModel request);
        Task<UserDto> DeleteUser(DeleteUserModel request);
        Task<List<UserDto>> GetAllUsers();
        Task<List<UserDto>> GetRecentlyRegisteredUser();
    }
}
