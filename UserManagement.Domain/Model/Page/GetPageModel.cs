using System;
using UserManagement.Data.Dto.Page;

namespace UserManagement.Domain.Model.Page
{
    public class GetPageModel : PageDto
    {
        public Guid Id { get; set; }
    }
}
