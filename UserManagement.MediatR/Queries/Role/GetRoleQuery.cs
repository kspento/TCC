using MediatR;
using System;
using UserManagement.Helper;
using UserManagement.Data.Dto.Role;

namespace UserManagement.MediatR.Queries
{
    public class GetRoleQuery: IRequest<ServiceResponse<RoleDto>>
    {
        public Guid Id { get; set; }
    }
}
