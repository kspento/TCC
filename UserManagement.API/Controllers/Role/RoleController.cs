using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Data.Dto.Role;
using UserManagement.Domain.Model.Role;

namespace UserManagement.API.Controllers
{
    /// <summary>
    /// Role
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : BaseController
    {
        public IRoleService _roleService { get; set; }
        /// <summary>
        /// Role
        /// </summary>
        /// <param name="roleService"></param>
        public RoleController(
            IRoleService roleService)
        {
            _roleService = roleService;
        }
        /// <summary>
        /// Create A Role
        /// </summary>
        /// <param name="addRoleCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(RoleDto))]
        public async Task<IActionResult> AddRole(AddRoleModel addRoleModel)
        {
            var result = await _roleService.AddRole(addRoleModel);

            return CreatedAtAction("GetRole", new { id = result.Id }, result);
        }
        /// <summary>
        /// Update Exist Role By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateRoleCommand"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(RoleDto))]
        public async Task<IActionResult> UpdateRole(Guid id, UpdateRoleModel updateRoleModel)
        {
            updateRoleModel.Id = id;
            var result = await _roleService.UpdateRole(updateRoleModel);
            return CreateApiResponse(result);
        }
        /// <summary>
        /// Get Role By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}", Name = "GetRole")]
        [Produces("application/json", "application/xml", Type = typeof(RoleDto))]
        public async Task<IActionResult> GetRole(Guid id)
        {
            var result = await _roleService.GetRole(id);
            return CreateApiResponse(result);
        }
        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetRoles")]
        [Produces("application/json", "application/xml", Type = typeof(List<RoleDto>))]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _roleService.GetAllRoles();
            return Ok(result);
        }
        /// <summary>
        /// Delete Role By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            await _roleService.DeleteRole(id);
            return Ok();
        }
    }
}
