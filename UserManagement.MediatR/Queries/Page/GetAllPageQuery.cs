using MediatR;
using System.Collections.Generic;
using UserManagement.Data.Dto.Page;

namespace UserManagement.MediatR.Queries
{
    public class GetAllPageQuery : IRequest<List<PageDto>>
    {
    }
}
