using MediatR;
using System.Collections.Generic;
using UserManagement.Data.Dto.PageAction;

namespace UserManagement.MediatR.Queries
{
    public class GetAllPageActionQuery : IRequest<List<PageActionDto>>
    {
    }
}
