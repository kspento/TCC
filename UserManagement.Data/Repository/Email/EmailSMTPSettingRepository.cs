using UserManagement.Data.Context;
using UserManagement.Data.Entities;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Data.UnitOfWork;

namespace UserManagement.Data.Repository.Email
{
    public class EmailSMTPSettingRepository : GenericRepository<EmailSMTPSetting, UserContext>,
           IEmailSMTPSettingRepository
    {
        public EmailSMTPSettingRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {
        }
    }
}
