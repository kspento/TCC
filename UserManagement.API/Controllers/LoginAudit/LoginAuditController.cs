using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.Data.Repository.LoginAudit;
using UserManagement.Data.Resources;
using UserManagement.Domain.Contracts.Services;
using UserManagement.Domain.Model.LoginAudit;

namespace UserManagement.API.Controllers.LoginAudit
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoginAuditController : ControllerBase
    {
        public ILoginAuditService _loginAuditService { get; set; }
        public LoginAuditController(ILoginAuditService loginAuditService)
        {
            _loginAuditService = loginAuditService;
        }
        /// <summary>
        /// Get All Login Audit detail
        /// </summary>
        /// <param name="loginAuditResource"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(LoginAuditList))]
        public async Task<IActionResult> GetLoginAudit([FromQuery] LoginAuditResource loginAuditResource)
        {
            var getAllLoginAuditQuery = new LoginAuditModel
            {
                Fields = loginAuditResource.Fields,
                OrderBy = loginAuditResource.OrderBy,
                PageSize = loginAuditResource.PageSize,
                SearchQuery = loginAuditResource.SearchQuery,
                Skip = loginAuditResource.Skip,
                UserName = loginAuditResource.UserName
            };
            var result = await _loginAuditService.GetAllLoginAudit(getAllLoginAuditQuery);

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
    }
}
