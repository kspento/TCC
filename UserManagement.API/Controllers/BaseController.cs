using UserManagement.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace UserManagement.API.Controllers
{
    public class BaseController : ControllerBase
    {
        //public IActionResult ReturnFormattedResponse<T>(ApiResponse<T> response)
        //{
        //    if (response.Success)
        //    {
        //        return Ok(response.Data);
        //    }
        //    return StatusCode(response.StatusCode, response.Errors);
        //}

        protected IActionResult CreateApiResponse<T>(T data, int statusCode = 200, List<string> errors = null)
        {
            var response = new ApiResponse<T>
            {
                Data = data,
                Success = errors == null || !errors.Any(),
                StatusCode = statusCode,
                Errors = errors ?? new List<string>()
            };

            return StatusCode(response.StatusCode, response);
        }
    }
}