using MediatR;
using System.Collections.Generic;
using UserManagement.Helper;
using UserManagement.Data.Dto.Action;

namespace UserManagement.MediatR.Queries
{
    public class GetAllActionQuery : IRequest<ServiceResponse<List<ActionDto>>>
    {
    }
}
