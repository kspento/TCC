using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.Domain.Contracts.Services;
using UserManagement.Domain.Model.Social;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SocialLoginController : BaseController
    {
        public ISocialLoginService _socialLoginService { get; set; }

        public SocialLoginController(ISocialLoginService socialLoginService)
        {
            _socialLoginService = socialLoginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(SocialLoginModel userLoginModel)
        {
            userLoginModel.RemoteIp = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var result = await _socialLoginService.SocialLogin(userLoginModel);

            if (!string.IsNullOrWhiteSpace(result.ProfilePhoto))
            {
                result.ProfilePhoto = $"Users/{result.ProfilePhoto}";
            }
            return Ok(result);
        }
    }
}
