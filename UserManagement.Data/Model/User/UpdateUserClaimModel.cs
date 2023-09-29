using System.Collections.Generic;
using System;
using UserManagement.Data.Dto.UserClaim;

namespace UserManagement.Domain.Model.User
{
    public class UpdateUserClaimModel
    {
        public Guid Id { get; set; }
        public List<UserClaimDto> UserClaims { get; set; }
    }
}
