using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using UserManagement.Data.Dto.AppSetting;
using UserManagement.Domain.Contracts.Services;
using UserManagement.Domain.Model.App;

namespace UserManagement.API.Controllers.AppSetting
{
    /// <summary>
    /// App Setting
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AppSettingController : BaseController
    {
        public IAppSettingService _appSettingService { get; set; }
        private readonly ILogger<AppSettingController> _logger;
        /// <summary>
        /// App Setting
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public AppSettingController(
            IAppSettingService appSettingService,
            ILogger<AppSettingController> logger)
        {
            _appSettingService = appSettingService;
            _logger = logger;
        }
        /// <summary>
        /// Create  Appsetting
        /// </summary>
        /// <param name="addAppSettingModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(AppSettingDto))]
        public async Task<IActionResult> AddAppSetting(AddAppSettingModel addAppSettingModel)
        {
            var result = await _appSettingService.AddAppSetting(addAppSettingModel);


            //if (result.StatusCode != 200)
            //{
            //    _logger.LogError(result.StatusCode,
            //                    JsonSerializer.Serialize(result), "");
            //    return StatusCode(result.StatusCode, result);
            //}
            //if (!result.Success)
            //{
            //    return ReturnFormattedResponse(result);
            //}
            return CreatedAtAction("GetAppSetting", new { id = result.Id }, result);
        }
        /// <summary>
        /// Update Exist AppSetting By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAppSettingCommand"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(AppSettingDto))]
        public async Task<IActionResult> UpdateAppSetting(Guid id, UpdateAppSettingModel updateAppSettingCommand)
        {
            updateAppSettingCommand.Id = id;
            var result = await _appSettingService.UpdateAppSetting(updateAppSettingCommand);
            return CreateApiResponse(result);
        }
        /// <summary>
        /// Get AppSetting By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}", Name = "GetAppSetting")]
        [Produces("application/json", "application/xml", Type = typeof(AppSettingDto))]
        public async Task<IActionResult> GetAppSetting(Guid id)
        {
            _logger.LogTrace("GetAppSetting");
            var getAppSettingQuery = new GetAppSettingModel
            {
                Id = id
            };

            var result = await _appSettingService.GetAppSetting(getAppSettingQuery);
            return CreateApiResponse(result);

        }
        /// <summary>
        /// Get AppSetting By Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>

        [HttpGet("key/{id}", Name = "GetAppSettingByKey")]
        [Produces("application/json", "application/xml", Type = typeof(AppSettingDto))]
        public async Task<IActionResult> GetAppSettingByKey(string key)
        {
            _logger.LogTrace("GetAppSettingByKey");
            var getAppSettingByKeyQuery = new GetAppSettingModel
            {
                Key = key
            };

            var result = await _appSettingService.GetAppSettingByKey(getAppSettingByKeyQuery);
            return CreateApiResponse(result);
        }
        /// <summary>
        /// Get All AppSettings
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetAppSettings")]
        [Produces("application/json", "application/xml", Type = typeof(List<AppSettingDto>))]
        public async Task<IActionResult> GetAppSettings()
        {
            var getAllAppSettingQuery = new GetAppSettingModel
            {
            };
            var result = await _appSettingService.GetAppSettingByKey(getAllAppSettingQuery);
            return CreateApiResponse(result);
        }
        /// <summary>
        /// Delete AppSetting By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAppSetting(Guid Id)
        {
            var deleteAppSettingCommand = new DeleteAppSettingModel
            {
                Id = Id
            };

            await _appSettingService.DeleteAppSetting(deleteAppSettingCommand);

            return NoContent();
        }
    }
}
