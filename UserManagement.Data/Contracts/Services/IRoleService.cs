using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Data.Dto.Role;
using UserManagement.Data.Dto.User;
using UserManagement.Domain.Model.Role;

public interface IRoleService
{
    Task<RoleDto> AddRole(AddRoleModel request);
    Task DeleteRole(DeleteRoleModel request);
    Task<List<RoleDto>> GetAllRoles(GetAllRoleModel request);
    Task<RoleDto> GetRole(GetRoleModel request);
    Task<List<UserRoleDto>> GetRoleUsers(GetRoleUsersModel request);
    Task<RoleDto> UpdateRole(UpdateRoleModel request);
}