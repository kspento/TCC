using UserManagement.Data.Context;
using UserManagement.Data.Entities;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Data.UnitOfWork;

namespace UserManagement.Repository
{
    public class UserAllowedIPRepository : GenericRepository<UserAllowedIP, UserContext>,
        IUserAllowedIPRepository
    {
        public UserAllowedIPRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {
        }
    }
}
