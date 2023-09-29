using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.MediatR.Commands;

namespace UserManagement.API.Controllers.Email
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : BaseController
    {
        IMediator _mediator;
        public EmailController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Send mail.
        /// </summary>
        /// <param name="sendEmailCommand"></param>
        /// <returns></returns>
        [HttpPost(Name = "SendEmail")]
        [Produces("application/json", "application/xml", Type = typeof(void))]
        public async Task<IActionResult> SendEmail(SendEmailCommand sendEmailCommand)
        {
            var result = await _mediator.Send(sendEmailCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
