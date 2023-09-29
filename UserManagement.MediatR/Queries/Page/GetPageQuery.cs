using MediatR;
using System;
using UserManagement.Helper;
using UserManagement.Data.Dto.Page;

namespace UserManagement.MediatR.Queries
{
    public class GetPageQuery : IRequest<ServiceResponse<PageDto>>
    {
        public Guid Id { get; set; }
    }
}
