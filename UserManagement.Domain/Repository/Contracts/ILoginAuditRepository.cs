using System.Threading.Tasks;
using UserManagement.Data.Dto.LoginAudit;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.Repository.LoginAudit;
using UserManagement.Data.Resources;

namespace UserManagement.Data.Repository.Contracts
{
    public interface ILoginAuditRepository : IGenericRepository<Entities.LoginAudit>
    {
        Task<LoginAuditList> GetDocumentAuditTrails(LoginAuditResource loginAuditResrouce);
        Task LoginAudit(LoginAuditDto loginAudit);
    }
}
