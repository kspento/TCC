using System;
using UserManagement.Data.Dto.Page;

namespace UserManagement.Domain.Model.Page
{
    public class DeletePageModel : PageDto
    {
        public Guid Id { get; set; }
    }
}
