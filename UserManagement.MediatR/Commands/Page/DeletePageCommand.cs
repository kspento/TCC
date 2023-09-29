using MediatR;
using System;
using UserManagement.Helper;
using UserManagement.Data.Dto.Page;

namespace UserManagement.MediatR.Commands
{
    public class DeletePageCommand : IRequest<ServiceResponse<PageDto>>
    {
        public Guid Id { get; set; }
    }
}
