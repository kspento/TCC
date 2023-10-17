using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Data.Dto.Email;
using UserManagement.Domain.Contracts.Services;
using UserManagement.Domain.Model.Email;

namespace UserManagement.API.Controllers.Email
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSMTPSettingController : BaseController
    {
        IEmailService _emailService;
        public EmailSMTPSettingController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        /// <summary>
        /// Create an Email SMTP Configuration.
        /// </summary>
        /// <param name="addEmailSMTPSetting"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(EmailSMTPSettingDto))]
        public async Task<IActionResult> AddEmailSMTPSetting(EmailSettingModel addEmailSMTPSetting)
        {
            var result = await _emailService.AddEmailSMTPSetting(addEmailSMTPSetting);

            return CreateApiResponse(result);
        }

        /// <summary>
        /// Get Email SMTP Configuration.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(EmailSMTPSettingDto))]
        public async Task<IActionResult> GetEmailSMTPSetting(Guid id)
        {
            var query = new EmailSettingModel() { Id = id };
            var result = await _emailService.GetEmailSMTPSetting(query);
            return CreateApiResponse(result);
        }

        /// <summary>
        /// Get Email SMTP Configuration list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(List<EmailSMTPSettingDto>))]
        public async Task<IActionResult> GetEmailSMTPSettings()
        {

            var result = await _emailService.GetEmailSMTPSettings();
            return CreateApiResponse(result);
        }

        /// <summary>
        /// Update an Email SMTP Configuration.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateEmailSMTPSettingCommand"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(EmailSMTPSettingDto))]
        public async Task<IActionResult> UpdateEmailSMTPSetting(Guid id, EmailSettingModel updateEmailSMTPSettingCommand)
        {
            updateEmailSMTPSettingCommand.Id = id;
            var result = await _emailService.UpdateEmailSMTPSetting(updateEmailSMTPSettingCommand);
            return CreateApiResponse(result);
        }

        /// <summary>
        /// Delete an Email SMTP Configuration.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(EmailSMTPSettingDto))]
        public async Task<IActionResult> DeleteEmailSMTPSetting(Guid id)
        {
            var deleteEmailSMTPSettingCommand = new EmailSettingModel() { Id = id };
             await _emailService.DeleteEmailSMTPSetting(deleteEmailSMTPSettingCommand);
            return NoContent();
        }
    }
}
