using System.Threading.Tasks;
using UserManagement.Data.Repository.LoginAudit;
using UserManagement.Domain.Model.LoginAudit;

namespace UserManagement.Domain.Contracts.Services
{
    public interface ILoginAuditService
    {
        Task<LoginAuditList> GetAllLoginAudit(LoginAuditModel request);
    }
}