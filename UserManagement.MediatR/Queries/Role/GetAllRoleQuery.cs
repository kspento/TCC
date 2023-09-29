using MediatR;
using System.Collections.Generic;
using UserManagement.Data.Dto.Role;

namespace UserManagement.MediatR.Queries
{
    public class GetAllRoleQuery : IRequest<List<RoleDto>>
    {
    }
}
