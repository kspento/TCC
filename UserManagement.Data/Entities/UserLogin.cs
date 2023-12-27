using Microsoft.AspNetCore.Identity;
using System;

namespace UserManagement.Data.Entities
{
    public class UserLogin : IdentityUserLogin<Guid>
    {
        public virtual User User { get; set; }
    }
}
