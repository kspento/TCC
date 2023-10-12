using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto.EmailTemplate;
using UserManagement.Domain.Model.EmailTemplate;
using UserManagement.Helper;

namespace UserManagement.Domain.Contracts.Services
{
    public interface IEmailTemplateService
    {
        Task<EmailTemplateDto> AddEmailTemplate(AddEmailTemplateModel request, CancellationToken cancellationToken);
        Task<bool> DeleteEmailTemplate(DeleteEmailTemplateModel request, CancellationToken cancellationToken);
        Task<List<EmailTemplateDto>> GetAllEmailTemplate(CancellationToken cancellationToken);
        Task<EmailTemplateDto> GetEmailTemplate(GetEmailTemplateModel request, CancellationToken cancellationToken);
        Task<EmailTemplateDto> UpdateEmailTemplate(UpdateEmailTemplateModel request, CancellationToken cancellationToken);
    }
}