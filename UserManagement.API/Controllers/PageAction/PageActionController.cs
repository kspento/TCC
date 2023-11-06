using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Data.Dto.PageAction;
using UserManagement.Domain.Contracts.Services;
using UserManagement.Domain.Model.PageAction;

namespace UserManagement.API.Controllers
{
    /// <summary>
    /// Page Action
    /// </summary>
    [Route("api")]
    [ApiController]
    [Authorize]
    public class PageActionController : BaseController
    {
        public IPageActionService _pageActionService { get; set; }
        /// <summary>
        /// Page Action
        /// </summary>
        /// <param name="mediator"></param>
        public PageActionController(IPageActionService pageActionService)
        {
            _pageActionService = pageActionService;
        }
        /// <summary>
        /// Get All Page Actions
        /// </summary>
        /// <returns></returns>
        [HttpGet("PageActions")]
        [Produces("application/json", "application/xml", Type = typeof(List<PageActionDto>))]
        public async Task<IActionResult> GetPageActions()
        {
            var result = await _pageActionService.GetAllPageActions();
            return Ok(result);
        }
        /// <summary>
        /// Add Page Action
        /// </summary>
        /// <param name="AddPageActionModel"></param>
        /// <returns></returns>
        [HttpPost("PageAction")]
        [Produces("application/json", "application/xml", Type = typeof(PageActionDto))]
        public async Task<IActionResult> AddPageAction(AddPageActionModel addPageActionModel)
        {
            var result = await _pageActionService.AddPageAction(addPageActionModel);
            return CreateApiResponse(result);
        }

        /// <summary>
        /// Delete Page Action By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("PageAction/{Id}")]
        public async Task<IActionResult> DeletePageAction(Guid id)
        {
            await _pageActionService.DeletePageAction(id);
            return Ok();
        }
    }
}
