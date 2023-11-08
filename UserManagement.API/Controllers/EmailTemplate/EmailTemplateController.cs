using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Data.Dto.AppSetting;
using UserManagement.Data.Dto.EmailTemplate;
using UserManagement.Domain.Contracts.Services;
using UserManagement.Domain.Model.EmailTemplate;

namespace UserManagement.API.Controllers.EmailTemplate
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTemplateController : BaseController
    {
        public IEmailTemplateService _emailTemplateService { get; set; }
        private readonly ILogger<EmailTemplateController> _logger;
        /// <summary>
        /// Role
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public EmailTemplateController(
            IEmailTemplateService emailTemplateService,
            ILogger<EmailTemplateController> logger)
        {
            _emailTemplateService = emailTemplateService;
            _logger = logger;
        }
        /// <summary>
        /// Create  Email Template
        /// </summary>
        /// <param name="addEmailTemplate"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(EmailTemplateDto))]
        public async Task<IActionResult> AddEmailTemplate(AddEmailTemplateModel addEmailTemplate)
        {
            var result = await _emailTemplateService.AddEmailTemplate(addEmailTemplate);

            return CreatedAtAction("GetEmailTemplate", new { id = result.Id }, result);
        }
        /// <summary>
        /// Update Exist AppSetting By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateEmailTemplateCommand"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(AppSettingDto))]
        public async Task<IActionResult> UpdateAppSetting(Guid id, UpdateEmailTemplateModel updateEmailTemplate)
        {
            updateEmailTemplate.Id = id;
            var result = await _emailTemplateService.UpdateEmailTemplate(updateEmailTemplate);
            return CreateApiResponse(result);
        }
        /// <summary>
        /// Get Email Template By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}", Name = "GetEmailTemplate")]
        [Produces("application/json", "application/xml", Type = typeof(EmailTemplateDto))]
        public async Task<IActionResult> GetEmailTemplate(Guid id)
        {
            _logger.LogTrace("GetAppSetting");
            var getEmailTemplateQuery = new GetEmailTemplateModel
            {
                Id = id
            };

            var result = await _emailTemplateService.GetEmailTemplate(getEmailTemplateQuery);
            return Ok(result);

        }
        /// <summary>
        /// Get All Email Templates
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetEmailTemplates")]
        [Produces("application/json", "application/xml", Type = typeof(List<EmailTemplateDto>))]
        public async Task<IActionResult> GetEmailTemplates()
        {

            var result = await _emailTemplateService.GetAllEmailTemplate();
            return Ok(result);
        }
        /// <summary>
        /// Delete Email Template By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DelterEmailTemplate(Guid Id)
        {
            var deleteEmailTemplateCommand = new DeleteEmailTemplateModel
            {
                Id = Id
            };
            var result = await _emailTemplateService.DeleteEmailTemplate(deleteEmailTemplateCommand);
            return CreateApiResponse(result);
        }
    }
}
