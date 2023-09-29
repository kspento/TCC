using UserManagement.Data.Context;
using UserManagement.Data.Entities;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.UnitOfWork;

namespace UserManagement.Repository
{
    public class EmailTemplateRepository : GenericRepository<EmailTemplate, UserContext>,
          IEmailTemplateRepository
    {
        public EmailTemplateRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {

        }
    }
}

