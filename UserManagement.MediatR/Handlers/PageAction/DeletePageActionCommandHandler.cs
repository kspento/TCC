using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;
using UserManagement.Data.Context;
using UserManagement.Data.Dto.PageAction;
using UserManagement.Data.UnitOfWork;
using UserManagement.Data.Repository.PageAction;
using UserManagement.Domain.Model.PageAction;

namespace UserManagement.MediatR.Handlers
{
    public class DeletePageActionCommandHandler : IRequestHandler<DeletePageActionModel, ServiceResponse<PageActionDto>>
    {
        private readonly IPageActionRepository _pageActionRepository;
        private readonly IUnitOfWork<UserContext> _uow;
        private readonly IMapper _mapper;
        public DeletePageActionCommandHandler(
           IPageActionRepository pageActionRepository,
            IMapper mapper,
            IUnitOfWork<UserContext> uow
            )
        {
            _pageActionRepository = pageActionRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<PageActionDto>> Handle(DeletePageActionModel request, CancellationToken cancellationToken)
        {
            var entityExist = await _pageActionRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            if (entityExist == null)
            {
                return ServiceResponse<PageActionDto>.Return404();
            }
            _pageActionRepository.Remove(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<PageActionDto>.Return500();
            }
            return ServiceResponse<PageActionDto>.ReturnSuccess();
        }
    }
}
