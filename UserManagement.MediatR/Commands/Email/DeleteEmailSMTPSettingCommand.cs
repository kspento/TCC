using MediatR;
using System;
using UserManagement.Data.Dto.Email;
using UserManagement.Helper;

namespace UserManagement.MediatR.Commands
{
    public class DeleteEmailSMTPSettingCommand : IRequest<ServiceResponse<EmailSMTPSettingDto>>
    {
        public Guid Id { get; set; }
    }
}
