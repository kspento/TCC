using MediatR;
using System.Collections.Generic;
using UserManagement.Helper;
using UserManagement.Data.Dto.RoleClaim;
using UserManagement.Data.Dto.Role;

namespace UserManagement.MediatR.Commands
{
    public class AddRoleCommand : IRequest<ServiceResponse<RoleDto>>
    {
        public string Name { get; set; }
        public List<RoleClaimDto> RoleClaims { get; set; }
    }
}
