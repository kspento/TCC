using UserManagement.Data.Dto.EmailTemplate;

namespace UserManagement.Domain.Model.EmailTemplate
{
    public class AddEmailTemplateModel : EmailTemplateDto
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
