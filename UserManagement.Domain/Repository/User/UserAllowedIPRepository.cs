using UserManagement.Data.Context;
using UserManagement.Data.Entities;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.UnitOfWork;

namespace UserManagement.Data.Repository.User
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
