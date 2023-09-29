using MediatR;
using System;
using UserManagement.Data.Dto.EmailTemplate;
using UserManagement.Helper;

namespace UserManagement.MediatR.Commands
{
    public class UpdateEmailTemplateCommand : IRequest<ServiceResponse<EmailTemplateDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
