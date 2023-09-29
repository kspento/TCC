using MediatR;
using System;
using System.Collections.Generic;
using UserManagement.Helper;
using UserManagement.Data.Dto.RoleClaim;
using UserManagement.Data.Dto.Role;

namespace UserManagement.MediatR.Commands
{
    public class UpdateRoleCommand: IRequest<ServiceResponse<RoleDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<RoleClaimDto> RoleClaims { get; set; }
    }
}
