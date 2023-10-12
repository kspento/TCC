using System;
using UserManagement.Data.Dto.PageAction;

namespace UserManagement.Domain.Model.PageAction
{
    public class DeletePageActionModel : PageActionDto
    {
        public Guid Id { get; set; }
        public Guid PageId { get; set; }
        public Guid ActionId { get; set; }
    }
}
