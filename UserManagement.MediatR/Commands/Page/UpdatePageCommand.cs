using MediatR;
using System;
using UserManagement.Helper;
using UserManagement.Data.Dto.Page;

namespace UserManagement.MediatR.Commands
{
    public class UpdatePageCommand : IRequest<ServiceResponse<PageDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
