using MediatR;
using System;
using System.Collections.Generic;
using UserManagement.Helper;
using UserManagement.Data.Dto.UserClaim;

namespace UserManagement.MediatR.Commands
{
    public class UpdateUserClaimCommand : IRequest<ServiceResponse<UserClaimDto>>
    {
        public Guid Id { get; set; }
        public List<UserClaimDto> UserClaims { get; set; }
    }
}
