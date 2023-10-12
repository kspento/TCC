using System;
using UserManagement.Data.Dto.User;

namespace UserManagement.Domain.Model.Role
{
    public class GetRoleUsersModel : UserRoleDto
    {
        public Guid RoleId { get; set; }
    }
}
