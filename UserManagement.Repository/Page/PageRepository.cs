using UserManagement.Data.Context;
using UserManagement.Data.Entities;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.UnitOfWork;

namespace UserManagement.Repository
{
    public class PageRepository : GenericRepository<Page, UserContext>,
          IPageRepository
    {
        public PageRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {
        }
    }
}
