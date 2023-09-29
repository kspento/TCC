using AutoMapper;
using UserManagement.MediatR.Commands;
using UserManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;
using UserManagement.Data.Context;
using UserManagement.Data.Entities;
using UserManagement.Data.Dto.UserClaim;
using UserManagement.Data.UnitOfWork;

namespace UserManagement.MediatR.Handlers
{
    public class UpdateUserClaimCommandHandler : IRequestHandler<UpdateUserClaimCommand, ServiceResponse<UserClaimDto>>
    {
        IUnitOfWork<UserContext> _uow;
        IUserClaimRepository _userClaimRepository;
        private readonly IMapper _mapper;
        public UpdateUserClaimCommandHandler(
            IMapper mapper,
            IUserClaimRepository userClaimRepository,
            IUnitOfWork<UserContext> uow
            )
        {
            _mapper = mapper;
            _uow = uow;
            _userClaimRepository = userClaimRepository;
        }

        public async Task<ServiceResponse<UserClaimDto>> Handle(UpdateUserClaimCommand request, CancellationToken cancellationToken)
        {
            var appUserClaims = await _userClaimRepository.All.Where(c => c.UserId == request.Id).ToListAsync();
            var claimsToAdd = request.UserClaims.Where(c => !appUserClaims.Select(c => c.ClaimType).Contains(c.ClaimType)).ToList();
            _userClaimRepository.AddRange(_mapper.Map<List<UserClaim>>(claimsToAdd));
            var claimsToDelete = appUserClaims.Where(c => !request.UserClaims.Select(cs => cs.ClaimType).Contains(c.ClaimType)).ToList();
            _userClaimRepository.RemoveRange(claimsToDelete);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<UserClaimDto>.Return500();
            }
            return ServiceResponse<UserClaimDto>.ReturnSuccess();
        }
    }
}
