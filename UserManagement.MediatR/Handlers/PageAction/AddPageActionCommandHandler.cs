using AutoMapper;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data;
using UserManagement.Data.Dto;
using UserManagement.Domain;
using UserManagement.MediatR.Commands;
using UserManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;

namespace UserManagement.MediatR.Handlers
{
    public class AddPageActionCommandHandler : IRequestHandler<AddPageActionCommand, ServiceResponse<PageActionDto>>
    {
        private readonly IPageActionRepository _pageActionRepository;
        private readonly IUnitOfWork<UserContext> _uow;
        private readonly IMapper _mapper;
        public AddPageActionCommandHandler(
           IPageActionRepository pageActionRepository,
            IMapper mapper,
            IUnitOfWork<UserContext> uow
            )
        {
            _pageActionRepository = pageActionRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<PageActionDto>> Handle(AddPageActionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _pageActionRepository.FindBy(c => c.PageId == request.PageId && c.ActionId == request.ActionId).FirstOrDefaultAsync();
            if (entity == null)
            {
                entity = _mapper.Map<PageAction>(request);
                entity.Id = Guid.NewGuid();
                _pageActionRepository.Add(entity);
                if (await _uow.SaveAsync() <= 0)
                {
                    return ServiceResponse<PageActionDto>.Return500();
                }
            }
            return ServiceResponse<PageActionDto>.ReturnResultWith200(_mapper.Map<PageActionDto>(entity));
        }
    }
}
