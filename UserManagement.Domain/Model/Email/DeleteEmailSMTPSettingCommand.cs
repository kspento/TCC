using System;
using UserManagement.Data.Dto.Email;

namespace UserManagement.Domain.Model.Email
{
    public class DeleteEmailSMTPSettingCommand : EmailSMTPSettingDto
    {
        public Guid Id { get; set; }
    }
}
