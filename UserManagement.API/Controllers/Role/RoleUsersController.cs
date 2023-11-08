using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Data.Dto.User;
using UserManagement.Domain.Model.Role;

namespace UserManagement.API.Controllers
{
    /// <summary>
    /// RoleUsers
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleUsersController : BaseController
    {
        public IRoleService _roleService { get; set; }
        /// <summary>
        /// RoleUsers
        /// </summary>
        /// <param name="mediator"></param>
        public RoleUsersController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        /// <summary>
        /// Get Role Users By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "RoleUsers")]
        [Produces("application/json", "application/xml", Type = typeof(List<UserRoleDto>))]
        public async Task<IActionResult> RoleUsers(Guid id)
        {
            //var result = await _roleService.GetRole(id);
            var result = await _roleService.GetRoleUsers(id);
            return Ok(result);
        }
        /// <summary>
        /// Update Role Users By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateRoleCommand"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(UserRoleDto))]
        public async Task<IActionResult> UpdateRoleUsers(Guid id, UpdateRoleModel updateRoleModel)
        {
            updateRoleModel.Id = id;
            //var result = await  _roleService.UpdateRole(updateRoleModel);

            await _roleService.UpdateRoleUser(updateRoleModel);

            return Ok();
        }
    }
}
