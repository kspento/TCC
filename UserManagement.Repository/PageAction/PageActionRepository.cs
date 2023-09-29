using UserManagement.Data.Context;
using UserManagement.Data.Entities;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.UnitOfWork;

namespace UserManagement.Repository
{
    public class PageActionRepository : GenericRepository<PageAction, UserContext>,
        IPageActionRepository
    {
        public PageActionRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {
        }
    }
}
