using UserManagement.Data.Context;
using UserManagement.Data.Entities;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.UnitOfWork;

namespace UserManagement.Data.Repository.PageAction
{
    public class PageActionRepository : GenericRepository<Entities.PageAction, UserContext>,
        IPageActionRepository
    {
        public PageActionRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {
        }
    }
}
