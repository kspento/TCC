using UserManagement.Data.Context;
using UserManagement.Data.Entities;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Data.UnitOfWork;

namespace UserManagement.Data.Repository.AppSetting
{
    public class AppSettingRepository : GenericRepository<Entities.AppSetting, UserContext>,
          IAppSettingRepository
    {
        public AppSettingRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {

        }
    }
}
