using MediatR;
using System;
using UserManagement.Helper;
using UserManagement.Data.Dto.PageAction;

namespace UserManagement.MediatR.Queries
{
    public class GetPageActionQuery : IRequest<ServiceResponse<PageActionDto>>
    {
        public Guid Id { get; set; }
    }
}
