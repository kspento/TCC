using UserManagement.Data.Context;
using UserManagement.Data.Entities;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.UnitOfWork;

namespace UserManagement.Data.Repository.RoleClaim
{
    public class RoleClaimRepository : GenericRepository<Entities.RoleClaim, UserContext>,
           IRoleClaimRepository
    {
        public RoleClaimRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {
        }
    }
}