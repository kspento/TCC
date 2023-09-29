using System.Threading.Tasks;
using UserManagement.Data.Dto.LoginAudit;
using UserManagement.Data.Entities;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.Resources;

namespace UserManagement.Repository
{
    public interface ILoginAuditRepository : IGenericRepository<LoginAudit>
    {
        Task<LoginAuditList> GetDocumentAuditTrails(LoginAuditResource loginAuditResrouce);
        Task LoginAudit(LoginAuditDto loginAudit);
    }
}
