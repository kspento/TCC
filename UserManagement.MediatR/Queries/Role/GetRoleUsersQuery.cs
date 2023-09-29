using MediatR;
using System;
using System.Collections.Generic;
using UserManagement.Data.Dto.User;

namespace UserManagement.MediatR.Queries
{
    public class GetRoleUsersQuery : IRequest<List<UserRoleDto>>
    {
        public Guid RoleId { get; set; }
    }
}
