using UserManagement.Data.Context;
using UserManagement.Data.Entities;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Data.UnitOfWork;

namespace UserManagement.Data.Repository.UserRole
{
    public class UserRoleRepository : GenericRepository<UserRole, UserContext>,
       IUserRoleRepository
    {
        public UserRoleRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {
        }
    }
}
