using MediatR;
using System;
using UserManagement.Helper;
using UserManagement.Data.Dto.Action;

namespace UserManagement.MediatR.Queries
{
    public class GetActionQuery : IRequest<ServiceResponse<ActionDto>>
    {
        public Guid Id { get; set; }
    }
}
