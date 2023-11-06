using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Data.Dto.Page;
using UserManagement.Domain.Contracts.Services;
using UserManagement.Domain.Model.Page;

namespace UserManagement.API.Controllers
{
    /// <summary>
    /// Page
    /// </summary>
    [Route("api")]
    [ApiController]
    [Authorize]
    public class PageController : BaseController
    {
        public IPageService _pageService { get; set; }

        /// <summary>
        /// Page
        /// </summary>
        /// <param name="pageService"></param>
        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }
        /// <summary>
        /// Get Page By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Page/{id}", Name = "GetPage")]
        [Produces("application/json", "application/xml", Type = typeof(PageDto))]
        public async Task<IActionResult> GetPage(Guid id)
        {

            var result = await _pageService.GetPage(id);
            return CreateApiResponse(result);
        }
        /// <summary>
        /// Get All Pages
        /// </summary>
        /// <returns>Test</returns>
        /// <response code="200">Returns the newly created item</response>
        [HttpGet("Pages")]
        [Produces("application/json", "application/xml", Type = typeof(List<PageDto>))]
        public async Task<IActionResult> GetPages()
        {            
            var result = await _pageService.GetAllPages();
            return Ok(result);
        }
        /// <summary>
        /// Create a Page
        /// </summary>
        /// <param name="addPageModel"></param>
        /// <returns></returns>
        [HttpPost("Page")]
        [Produces("application/json", "application/xml", Type = typeof(PageDto))]
        public async Task<IActionResult> AddPage(AddPageModel addPageModel)
        {
            var result = await _pageService.AddPage(addPageModel);

            return CreatedAtAction("GetPage", new { id = result.Id }, result);
        }
        /// <summary>
        /// Update Page By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="updatePageModel"></param>
        /// <returns></returns>
        [HttpPut("Page/{Id}")]
        [Produces("application/json", "application/xml", Type = typeof(PageDto))]
        public async Task<IActionResult> UpdatePage(Guid Id, UpdatePageModel updatePageModel)
        {
            updatePageModel.Id = Id;
            var result = await _pageService.UpdatePage(updatePageModel);
            return CreateApiResponse(result);
        }
        /// <summary>
        /// Delete Page By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Page/{Id}")]
        public async Task<IActionResult> DeletePage(Guid id)
        {
            await _pageService.DeletePage(id);               

            return Ok();
        }
    }
}
