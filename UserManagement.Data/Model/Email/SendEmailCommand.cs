using System.Collections.Generic;
using UserManagement.Data.Dto.Email;
using UserManagement.Helper;

namespace UserManagement.Domain.Model.Email
{
    public class SendEmailCommand : EmailDto
    {
        public string Subject { get; set; }
        public string ToAddress { get; set; }
        public string CCAddress { get; set; }
        public List<FileInfo> Attachments { get; set; } = new List<FileInfo>();
        public string Body { get; set; }
        public string FromAddress { get; set; }
    }

}
