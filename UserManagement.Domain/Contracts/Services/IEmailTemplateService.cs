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
        Task<EmailTemplateDto> AddEmailTemplate(AddEmailTemplateModel request);
        Task<bool> DeleteEmailTemplate(DeleteEmailTemplateModel request);
        Task<List<EmailTemplateDto>> GetAllEmailTemplate();
        Task<EmailTemplateDto> GetEmailTemplate(GetEmailTemplateModel request);
        Task<EmailTemplateDto> UpdateEmailTemplate(UpdateEmailTemplateModel request);
    }
}