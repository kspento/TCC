
using System.Threading.Tasks;
using System.Threading;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Data.Repository.LoginAudit;
using UserManagement.Domain.Model.LoginAudit;
using UserManagement.Domain.Contracts.Services;

namespace UserManagement.Domain.Services
{
    public class LoginAuditService : ILoginAuditService
    {
        private readonly ILoginAuditRepository _loginAuditRepository;
        public LoginAuditService(ILoginAuditRepository loginAuditRepository)
        {
            _loginAuditRepository = loginAuditRepository;
        }
        public async Task<LoginAuditList> GetAllLoginAudit(LoginAuditModel request)
        {
            return await _loginAuditRepository.GetDocumentAuditTrails(request);
        }
    }
}
