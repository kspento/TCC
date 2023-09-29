using System;

namespace UserManagement.Data.Entities
{
    public class UserAllowedIP
    {
        public Guid UserId { get; set; }
        public string IPAddress { get; set; }
        public User User { get; set; }
    }
}
