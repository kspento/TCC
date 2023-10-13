using UserManagement.Data.Context;
using UserManagement.Data.Entities;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Data.UnitOfWork;

namespace UserManagement.Data.Repository.EmailTemplate
{
    public class EmailTemplateRepository : GenericRepository<Entities.EmailTemplate, UserContext>,
          IEmailTemplateRepository
    {
        public EmailTemplateRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {

        }
    }
}

