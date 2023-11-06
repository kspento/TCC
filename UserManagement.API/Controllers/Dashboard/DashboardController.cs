using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto.User;
using UserManagement.Domain.Contracts.Services;

namespace UserManagement.API.Controllers.Dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {

        public IDashBoardService _dashBoardService { get; set; }

        public DashboardController(IDashBoardService dashBoardService)
        {
            _dashBoardService = dashBoardService;
        }

        /// <summary>
        /// Get Active User Count
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetActiveUserCount")]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        public async Task<IActionResult> GetActiveUserCount()
        {
            var cancelationToken = new CancellationToken();
            var result = await _dashBoardService.GetActiveUserCount(cancelationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get Inactive User Count
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetInactiveUserCount")]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        public async Task<IActionResult> GetInactiveUserCount()
        {
            var cancelationToken = new CancellationToken();
            var result = await _dashBoardService.GetInactiveUserCount(cancelationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get Total user count
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTotalUserCount")]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        public async Task<IActionResult> GetTotalUserCount()        
        {
            var cancelationToken = new CancellationToken();

            var result = await _dashBoardService.GetTotalUserCount(cancelationToken);
            return Ok(result);
        }

        /// <summary>
        /// Gets the online users.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetOnlineUsers")]
        [Produces("application/json", "application/xml", Type = typeof(List<UserDto>))]
        public async Task<IActionResult> GetOnlineUsers()
        {            
            var result = await _dashBoardService.GetOnlineUsers();
            return Ok(result);
        }
    }
}
