using System;
using UserManagement.Data.Dto.PageAction;

namespace UserManagement.Domain.Model.PageAction
{
    public class AddPageActionModel : PageActionDto
    {
        public Guid PageId { get; set; }
        public Guid ActionId { get; set; }
        public bool Flag { get; set; }
    }
}
