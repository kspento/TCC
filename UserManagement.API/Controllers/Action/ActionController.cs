using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Data.Dto.Action;
using UserManagement.Domain.Contracts.Services;
using UserManagement.Domain.Model.Action;

namespace UserManagement.API.Controllers
{
    /// <summary>
    /// Action
    /// </summary>
    [Route("api")]
    [ApiController]
    [Authorize]
    [CustomExceptionFilter]
    public class ActionController : BaseController
    {
        public IActionService _actionService { get; set; }
        
        /// <summary>
        /// Action
        /// </summary>
        /// <param name="mediator"></param>
        public ActionController(IActionService actionService)
        {
            _actionService = actionService;
        }
        /// <summary>
        /// Get Action By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("Action/{id}", Name = "GetAction")]
        [Produces("application/json", "application/xml", Type = typeof(ActionDto))]
        public async Task<IActionResult> GetAction(Guid id)
        {
            var getActionQuery = new ActionModel { Id = id };
            var result = _actionService.GetAction(getActionQuery);

            return CreateApiResponse(result);
        }
        /// <summary>
        /// Get All Actions
        /// </summary>
        /// <returns></returns>
        [HttpGet("Actions")]
        [Produces("application/json", "application/xml", Type = typeof(List<ActionDto>))]
        public async Task<IActionResult> GetActions()
        {          
            var result = await _actionService.GetAllAction();
            return CreateApiResponse(result);
        }
        /// <summary>
        /// Create A Action
        /// </summary>
        /// <param name="addActionModel"></param>
        /// <returns></returns>
        [HttpPost("Action")]
        [Produces("application/json", "application/xml", Type = typeof(ActionDto))]
        public async Task<IActionResult> AddAction(ActionModel addActionModel)
        {
            var response = await _actionService.AddAction(addActionModel);
            //if (!response.Success)
            //{
            //    return ReturnFormattedResponse(response);
            //}
            return CreatedAtAction("GetAction", new { id = response.Id }, response);
        }
        /// <summary>
        /// Update Exist Action By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="updateActionCommand"></param>
        /// <returns></returns>
        [HttpPut("Action/{Id}")]
        [Produces("application/json", "application/xml", Type = typeof(ActionDto))]
        public async Task<IActionResult> UpdateAction(Guid Id, ActionModel updateActionCommand)
        {
            updateActionCommand.Id = Id;
            var result = await  _actionService.UpdateAction(updateActionCommand);
            return CreateApiResponse(result);
        }
        /// <summary>
        /// Delete Action By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("Action/{Id}")]
        public async Task<IActionResult> DeleteAction(Guid Id)
        {
            var deleteActionCommand = new ActionModel { Id = Id };
            await _actionService.DeleteAction(deleteActionCommand);

            return NoContent();
        }
    }
}
