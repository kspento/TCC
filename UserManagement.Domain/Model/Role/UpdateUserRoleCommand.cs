using System;
using System.Collections.Generic;
using UserManagement.Data.Dto.User;

namespace UserManagement.Domain.Model.Role
{
    public class UpdateUserRoleCommand :UserRoleDto
    {
        public Guid Id { get; set; }
        public List<UserRoleDto> UserRoles { get; set; }
    }
}
