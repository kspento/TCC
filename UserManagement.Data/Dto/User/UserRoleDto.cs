﻿using System;

namespace UserManagement.Data.Dto.User
{
    public class UserRoleDto
    {
        public Guid? UserId { get; set; }
        public Guid RoleId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
