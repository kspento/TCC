using Microsoft.AspNetCore.Identity;
using System;

namespace UserManagement.Data.Entities
{
    public class UserToken : IdentityUserToken<Guid>
    {
        public virtual User User { get; set; } = null;
    }
}
