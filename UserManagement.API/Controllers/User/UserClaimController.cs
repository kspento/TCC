using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.API.Filters;
using UserManagement.Data.Dto.UserClaim;
using UserManagement.Domain.Contracts.Services;
using UserManagement.Domain.Model.User;

namespace UserManagement.API.Controllers
{
    /// <summary>
    /// UserClaim
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(CustomExceptionFilter))]
    public class UserClaimController : BaseController
    {
        public IUserService _userService { get; set; }
        /// <summary>
        /// UserClaim
        /// </summary>
        /// <param name="userService"></param>
        public UserClaimController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Update User Claim By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="addUserCommand"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(UserClaimDto))]
        public async Task<IActionResult> UpdateUserClaim(Guid id, UpdateUserClaimModel addUserCommand)
        {
            addUserCommand.Id = id;
            var result = await _userService.UpdateUserClaim(addUserCommand);
            return Ok(result);
        }
    }
}
