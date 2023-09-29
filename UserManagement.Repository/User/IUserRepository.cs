using System.Threading.Tasks;
using UserManagement.Data.Entities;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.Resources;
using UserManagement.Data.Dto.User;

namespace UserManagement.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<UserList> GetUsers(UserResource userResource);
        Task<UserAuthDto> BuildUserAuthObject(User appUser);
    }
}
