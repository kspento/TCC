using MediatR;
using System;
using UserManagement.Helper;
using UserManagement.Data.Dto.User;

namespace UserManagement.MediatR.Commands
{
    public class DeleteUserCommand : IRequest<ServiceResponse<UserDto>>
    {
        public Guid Id { get; set; }
    }
}
