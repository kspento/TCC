using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using UserManagement.Domain.Contracts.Services;
using UserManagement.Data.Resources;
using UserManagement.Data.Dto.User;
using UserManagement.Domain.Model.User;
using UserManagement.Repository;

namespace UserManagement.API.Controllers
{
    /// <summary>
    /// User
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private IWebHostEnvironment _webHostEnvironment;
        public readonly UserInfoToken _userInfo;
        /// <summary>
        /// User
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="webHostEnvironment"></param>
        public UserController(
            IUserService userService,
            UserInfoToken userInfo,
            IWebHostEnvironment webHostEnvironment
            )
        {
            _userService = userService;
            _userInfo = userInfo;
            _webHostEnvironment = webHostEnvironment;
        }
        /// <summary>
        ///  Create a User
        /// </summary>
        /// <param name="addUserCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(UserDto))]
        public async Task<IActionResult> AddUser(AddUserModel addUserCommand)
        {
            var result = await _userService.AddUser(addUserCommand);
            //if (!result.Success)
            //{
            //    return ReturnFormattedResponse(result);
            //}
            //return CreatedAtAction("GetUser", new { id = result.Data.Id }, result.Data);

            return Ok(result);
        }


        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllUsers")]
        [Produces("application/json", "application/xml", Type = typeof(List<UserDto>))]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();
            return Ok(result);
        }

        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetUser")]
        [Produces("application/json", "application/xml", Type = typeof(UserDto))]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var getUserModel = new GetUserModel { Id = id };
            var result = await _userService.GetUser(getUserModel);
            //return ReturnFormattedResponse(result);

            return Ok(result);
        }

        /// <summary>
        /// Get Users
        /// </summary>
        /// <param name="userResource"></param>
        /// <returns></returns>
        [HttpGet("GetUsers")]
        [Produces("application/json", "application/xml", Type = typeof(UserList))]
        public async Task<IActionResult> GetUsers([FromQuery] UserResource userResource)
        {
            var getAllLoginAuditQuery = new GetUsersModel
            {
                UserResource = userResource
            };
            var result = await _userService.GetUsers(getAllLoginAuditQuery);
        
            //var paginationMetadata = new
            //{
            //    totalCount = result.TotalCount,
            //    pageSize = result.PageSize,
            //    skip = result.Skip,
            //    totalPages = result.TotalPages
            //};
            //Response.Headers.Add("X-Pagination",
            //    Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            return Ok(result);
        }

        /// <summary>
        /// Get Recently Registered Users
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRecentlyRegisteredUsers")]
        [Produces("application/json", "application/xml", Type = typeof(List<UserDto>))]
        public async Task<IActionResult> GetRecentlyRegisteredUsers()
        {
            //var getRecentlyRegisteredUserQuery = new GetRecentlyRegisteredUserQuery { };
            var result = await _userService.GetRecentlyRegisteredUser();
            return Ok(result);
        }


        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="userLoginCommand"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [Produces("application/json", "application/xml", Type = typeof(UserAuthDto))]
        public async Task<IActionResult> UserLogin(UserLoginModel userLoginCommand)
        {
            userLoginCommand.RemoteIp = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var result = await _userService.UserLogin(userLoginCommand);

            //if (!result.Success)
            //{
            //    return ReturnFormattedResponse(result);
            //}
            //if (!string.IsNullOrWhiteSpace(result.Data.ProfilePhoto))
            //{
            //    result.Data.ProfilePhoto = $"Users/{result.Data.ProfilePhoto}";
            //}
            //
            //return Ok(result.Data);

            return Ok(result);
        }

        /// <summary>
        /// Update User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateUserCommand"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(UserDto))]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserModel updateUserCommand)
        {
            updateUserCommand.Id = id;
            var result = await _userService.UpdateUser(updateUserCommand);
            //return ReturnFormattedResponse(result);

            return Ok(result);
        }

        /// <summary>
        /// Update Profile
        /// </summary>
        /// <param name="updateUserProfileCommand"></param>
        /// <returns></returns>
        [HttpPut("profile")]
        [Produces("application/json", "application/xml", Type = typeof(UserDto))]
        public async Task<IActionResult> UpdateUserProfile(UpdateUserProfileModel updateUserProfileCommand)
        {
            var result = await _userService.UpdateUserProfile(updateUserProfileCommand);
            //return ReturnFormattedResponse(result);

            return Ok(result);
        }

        /// <summary>
        /// Update Profile photo
        /// </summary>
        /// <returns></returns>
        [HttpPost("UpdateUserProfilePhoto"), DisableRequestSizeLimit]
        [Produces("application/json", "application/xml", Type = typeof(UserDto))]
        public async Task<IActionResult> UpdateUserProfilePhoto()
        {
            var updateUserProfilePhotoCommand = new UpdateUserProfilePhotoModel()
            {
                FormFile = Request.Form.Files,
                RootPath = _webHostEnvironment.WebRootPath
            };
            var result = await _userService.UpdateUserProfilePhoto(updateUserProfilePhotoCommand);
            //return ReturnFormattedResponse(result);

            return Ok(result);
        }

        /// <summary>
        /// Delete User By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteUser(Guid Id)
        {
            var deleteUserCommand = new DeleteUserModel { Id = Id };
            var result = await _userService.DeleteUser(deleteUserCommand);
            //return ReturnFormattedResponse(result);
            
            return Ok(result);
        }

        /// <summary>
        /// User Change Password
        /// </summary>
        /// <param name="resetPasswordCommand"></param>
        /// <returns></returns>
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel resetPasswordCommand)
        {
            var result = await _userService.ChangePassword(resetPasswordCommand);
            //return ReturnFormattedResponse(result);

            return Ok(result);
        }

        /// <summary>
        /// Reset Resetpassword
        /// </summary>
        /// <param name="newPasswordCommand"></param>
        /// <returns></returns>
        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel newPasswordCommand)
        {
            var result = await _userService.ResetPassword(newPasswordCommand);
            //return ReturnFormattedResponse(result);

            return Ok(result);
        }

        /// <summary>
        /// Get User Profile
        /// </summary>
        /// <returns></returns>
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var getUserQuery = new GetUserModel
            {
                Id = Guid.Parse(_userInfo.Id)
            };
            var result = await _userService.GetUser(getUserQuery);
            if (!string.IsNullOrWhiteSpace(result.ProfilePhoto))
            {
                result.ProfilePhoto = $"Users/{result.ProfilePhoto}";
            }
            //return ReturnFormattedResponse(result);
            
            return Ok(result);
        }

    }
}
