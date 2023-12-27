using UserManagement.Data.Context;
using UserManagement.Data.Entities;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Data.UnitOfWork;

namespace UserManagement.Data.Repository.UserClaimRepository
{
    public class UserClaimRepository : GenericRepository<UserClaim, UserContext>,
           IUserClaimRepository
    {
        public UserClaimRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {
        }
    }
}