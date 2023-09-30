using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto.User;
using UserManagement.Data.Dto.UserClaim;
using UserManagement.Domain.Model.User;

namespace UserManagement.Domain.Contracts.Services
{
    public interface IUserService
    {
        Task<UserDto> AddUser(AddUserModel addUser);
        Task<UserDto> ChangePassword(ChangePasswordModel request);
        Task<UserDto> DeleteUser(DeleteUserModel request);
        Task<List<UserDto>> GetAllUsers();
        Task<List<UserDto>> GetRecentlyRegisteredUser();
        Task<UserDto> GetUser(GetUserModel request);
        Task<UserDto> GetUsers(GetUsersModel request);
        Task<UserDto> ResetPassword(ResetPasswordModel request);
        Task<UserDto> UpdateUser(UpdateUserModel request);
        Task<UserClaimDto> UpdateUserClaim(UpdateUserClaimModel request);
        Task<UserDto> UpdateUserProfile(UpdateUserProfileModel request);
        Task<UserDto> UpdateUserProfilePhoto(UpdateUserProfilePhotoModel request);
        Task<UserAuthDto> UserLogin(UserLoginModel request);
    }
}
