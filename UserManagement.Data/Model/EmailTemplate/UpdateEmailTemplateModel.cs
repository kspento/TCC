using System;
using UserManagement.Data.Dto.EmailTemplate;

namespace UserManagement.Domain.Model.EmailTemplate
{
    public class UpdateEmailTemplateModel : EmailTemplateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
