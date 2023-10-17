using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.Domain.Contracts.Services;
using UserManagement.Domain.Model.Email;

namespace UserManagement.API.Controllers.Email
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : BaseController
    {
        private IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        /// <summary>
        /// Send mail.
        /// </summary>
        /// <param name="sendEmailCommand"></param>
        /// <returns></returns>
        [HttpPost(Name = "SendEmail")]
        [Produces("application/json", "application/xml", Type = typeof(void))]
        public async Task<IActionResult> SendEmail(SendEmailModel sendEmailModel)
        {
            var result = await _emailService.SendEmail(sendEmailModel);

            return CreateApiResponse(result);
        }
    }
}
