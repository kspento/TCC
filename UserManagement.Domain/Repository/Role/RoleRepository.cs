using UserManagement.Data.Context;
using UserManagement.Data.Entities;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Data.UnitOfWork;

namespace UserManagement.Data.Repository.Role
{
    public class RoleRepository : GenericRepository<Role, UserContext>,
          IRoleRepository
    {
        public RoleRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {
        }
    }
}