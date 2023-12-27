using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Data.Dto.Email;
using UserManagement.Domain.Model.Email;

namespace UserManagement.Domain.Contracts.Services
{
    public interface IEmailService
    {
        Task<EmailSMTPSettingDto> AddEmailSMTPSetting(EmailSettingModel request);
        Task DeleteEmailSMTPSetting(EmailSettingModel request);
        Task<EmailSMTPSettingDto> GetEmailSMTPSetting(EmailSettingModel request);
        Task<List<EmailSMTPSettingDto>> GetEmailSMTPSettings();
        Task<EmailDto> SendEmail(SendEmailModel request);
        Task<EmailSMTPSettingDto> UpdateEmailSMTPSetting(EmailSettingModel request);
    }
}