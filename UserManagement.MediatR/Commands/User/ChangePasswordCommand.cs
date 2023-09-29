using MediatR;
using UserManagement.Helper;
using UserManagement.Data.Dto.User;

namespace UserManagement.MediatR.Commands
{
    public class ChangePasswordCommand : IRequest<ServiceResponse<UserDto>>
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
