using System.Collections.Generic;
using UserManagement.Data.Dto.RoleClaim;
using UserManagement.Data.Dto.Role;

namespace UserManagement.Domain.Model.Role
{
    public class AddRoleModel : RoleDto
    {
        public string Name { get; set; }
        public List<RoleClaimDto> RoleClaims { get; set; }
    }
}
