using MediatR;
using System.Collections.Generic;
using UserManagement.Data.Dto.Email;

namespace UserManagement.MediatR.Queries
{
    public class GetEmailSMTPSettingsQuery : IRequest<List<EmailSMTPSettingDto>>
    {
    }
}
