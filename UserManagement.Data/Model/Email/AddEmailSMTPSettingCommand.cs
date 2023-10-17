using UserManagement.Data.Dto.Email;

namespace UserManagement.Domain.Model.Email
{
    public class AddEmailSMTPSettingCommand : EmailSMTPSettingDto
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsEnableSSL { get; set; }
        public int Port { get; set; }
        public bool IsDefault { get; set; }
    }
}
