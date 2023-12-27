using System;

namespace UserManagement.Data.Dto.User
{
    public class UserAllowedIPDto
    {
        public Guid? UserId { get; set; }
        public string IPAddress { get; set; }
    }
}
