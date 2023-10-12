using System;
using UserManagement.Data.Dto.PageAction;

namespace UserManagement.MediatR.Queries
{
    public class GetPageActionModel : PageActionDto
    {
        public Guid Id { get; set; }
    }
}
