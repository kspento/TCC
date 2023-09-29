using UserManagement.Data.Context;
using UserManagement.Data.Entities;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.UnitOfWork;

namespace UserManagement.Repository
{
    public class AppSettingRepository : GenericRepository<AppSetting, UserContext>,
          IAppSettingRepository
    {
        public AppSettingRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {

        }
    }
}
