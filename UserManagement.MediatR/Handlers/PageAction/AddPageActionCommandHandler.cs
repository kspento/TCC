using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;
using UserManagement.Data.Context;
using UserManagement.Data.Entities;
using UserManagement.Data.Dto.PageAction;
using UserManagement.Data.UnitOfWork;
using UserManagement.Data.Repository.PageAction;
using UserManagement.Domain.Model.PageAction;

namespace UserManagement.MediatR.Handlers
{
    public class AddPageActionCommandHandler : IRequestHandler<AddPageActionModel, ServiceResponse<PageActionDto>>
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

        public async Task<ServiceResponse<PageActionDto>> Handle(AddPageActionModel request, CancellationToken cancellationToken)
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
