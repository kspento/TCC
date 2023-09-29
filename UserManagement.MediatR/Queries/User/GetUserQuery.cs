using MediatR;
using System;
using UserManagement.Helper;
using UserManagement.Data.Dto.User;

namespace UserManagement.MediatR.Queries
{
    public class GetUserQuery : IRequest<ServiceResponse<UserDto>>
    {
        public Guid Id { get; set; }
    }
}
