using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Data.Dto.Role;
using UserManagement.Data.Dto.User;
using UserManagement.Domain.Model.Role;

public interface IRoleService
{
    Task<RoleDto> AddRole(AddRoleModel request);
    Task DeleteRole(Guid id);
    Task<List<RoleDto>> GetAllRoles();
    Task<RoleDto> GetRole(Guid id);
    Task<List<UserRoleDto>> GetRoleUsers(Guid roleId);
    Task<RoleDto> UpdateRole(UpdateRoleModel request);
    Task UpdateRoleUser(UpdateRoleModel request);
}