using UserManagement.Data.Context;
using UserManagement.Data.Entities;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.UnitOfWork;

namespace UserManagement.Repository
{
    public class RoleClaimRepository : GenericRepository<RoleClaim, UserContext>,
           IRoleClaimRepository
    {
        public RoleClaimRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {
        }
    }
}