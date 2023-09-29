using MediatR;
using System;
using UserManagement.Helper;
using UserManagement.Data.Dto.Role;

namespace UserManagement.MediatR.Commands
{
    public class DeleteRoleCommand : IRequest<ServiceResponse<RoleDto>>
    {
        public Guid Id { get; set; }
    }
}
