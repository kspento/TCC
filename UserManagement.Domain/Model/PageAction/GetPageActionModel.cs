using System;
using UserManagement.Data.Dto.PageAction;

namespace UserManagement.Domain.Model.PageAction
{
    public class GetPageActionModel : PageActionDto
    {
        public Guid Id { get; set; }
    }
}
