﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserManagement.Data.Dto.NLog;
using UserManagement.Data.Resources;
using UserManagement.Domain.Contracts.Services;
using UserManagement.Domain.Model.NLog;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NLogController : BaseController
    {
        public INLogService _nlogService { get; set; }
        public NLogController(INLogService nlogService)
        {
            _nlogService = nlogService;
        }
        /// <summary>
        /// Get System Logs
        /// </summary>
        /// <param name="nLogResource"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(GetNLogsModel))]
        public async Task<IActionResult> GetNLogs([FromQuery] NLogResource nLogResource)
        {
            var getAllLoginAuditQuery = new get
            {
                NLogResource = nLogResource
            };
            var result = await _mediator.Send(getAllLoginAuditQuery);

            var paginationMetadata = new
            {
                totalCount = result.TotalCount,
                pageSize = result.PageSize,
                skip = result.Skip,
                totalPages = result.TotalPages
            };
            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            return Ok(result);
        }

        /// <summary>
        /// Get Log By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(NLogDto))]
        public async Task<IActionResult> GetNLog(Guid id)
        {
            var getLogQuery = new GetLogQuery { Id = id };
            var result = await _mediator.Send(getLogQuery);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Create Log.
        /// </summary>
        /// <param name="addLogCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(NLogDto))]
        public async Task<IActionResult> CreatNLog(AddLogCommand addLogCommand)
        {
            var result = await _mediator.Send(addLogCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
