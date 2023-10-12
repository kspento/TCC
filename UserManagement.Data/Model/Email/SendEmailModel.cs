using System.Collections.Generic;
using System.IO;
using UserManagement.Data.Dto.Email;

namespace UserManagement.Domain.Model.Email
{
    public class SendEmailModel : EmailDto
    {
        public string Subject { get; set; }
        public string ToAddress { get; set; }
        public string CCAddress { get; set; }
        public List<Helper.FileInfo> Attachments { get; set; } = new List<Helper.FileInfo>();
        public string Body { get; set; }
        public string FromAddress { get; set; }
    }
}
