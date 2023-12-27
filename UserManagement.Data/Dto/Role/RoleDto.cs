using System;
using System.Collections.Generic;
using UserManagement.Data.Dto.RoleClaim;

namespace UserManagement.Data.Dto.Role
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<RoleClaimDto> RoleClaims { get; set; }

    }
}
