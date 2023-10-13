using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using UserManagement.API.Controllers;
using UserManagement.Domain.Exception;

namespace UserManagement.API.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            ApiResponse<object> response;

            if (context.Exception is NotFoundException notFoundException)
            {
                response = new ApiResponse<object>
                {
                    StatusCode = 404,
                    Errors = new[] { notFoundException.Message }
                };
            }
            else if (context.Exception is UnauthorizedException unauthorizedException)
            {
                response = new ApiResponse<object>
                {
                    StatusCode = 401,
                    Errors = new[] { unauthorizedException.Message }
                };
            }
            else if (context.Exception is NotAllowedException notAllowedException)
            {
                response = new ApiResponse<object>
                {
                    StatusCode = 422,
                    Errors = new[] { notAllowedException.Message }
                };
            }
            else
            {
                response = new ApiResponse<object>
                {
                    StatusCode = 500,
                    Errors = new[] { "An unexpected fault happened. Try again later." }
                };
            }

            context.Result = new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };

            // Optionally log the exception
            // ...

            base.OnException(context);
        }
    }
}
