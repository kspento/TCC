using MediatR;
using UserManagement.Helper;
using UserManagement.Data.Dto.User;

namespace UserManagement.MediatR.Commands
{
    public class ResetPasswordCommand : IRequest<ServiceResponse<UserDto>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
