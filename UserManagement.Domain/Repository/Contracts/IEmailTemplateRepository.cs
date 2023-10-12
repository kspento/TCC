using UserManagement.Data.Entities;
using UserManagement.Data.GenericRespository;

namespace UserManagement.Data.Repository.Contracts
{
    public interface IEmailTemplateRepository : IGenericRepository<UserManagement.Data.Entities.EmailTemplate>
    {
    }
}
