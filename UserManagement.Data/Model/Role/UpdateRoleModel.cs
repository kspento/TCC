using System;
using System.Collections.Generic;
using UserManagement.Data.Dto.RoleClaim;
using UserManagement.Data.Dto.Role;
using UserManagement.Data.Dto.User;

namespace UserManagement.Domain.Model.Role
{
    public class UpdateRoleModel : RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<RoleClaimDto> RoleClaims { get; set; }
        public List<UserRoleDto> UserRoles { get; set; }
    }
}
