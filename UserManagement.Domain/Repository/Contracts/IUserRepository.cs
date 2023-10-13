using System.Threading.Tasks;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.Resources;
using UserManagement.Repository;

namespace UserManagement.Data.Repository.Contracts
{
    public interface IUserRepository : IGenericRepository<Entities.User>
    {
        Task<UserList> GetUsers(UserResource userResource);
        Task<UserManagement.Data.Dto.User.UserAuthDto> BuildUserAuthObject(Entities.User appUser);
    }
}
