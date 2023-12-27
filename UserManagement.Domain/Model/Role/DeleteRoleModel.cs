using System;
using UserManagement.Data.Dto.Role;

namespace UserManagement.Domain.Model.Role
{
    public class DeleteRoleModel : RoleDto
    {
        public Guid Id { get; set; }
    }
}
