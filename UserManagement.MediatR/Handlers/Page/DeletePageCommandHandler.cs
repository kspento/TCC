using UserManagement.MediatR.Commands;
using UserManagement.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;
using UserManagement.Data.Context;
using UserManagement.Data.Dto.Page;
using UserManagement.Data.UnitOfWork;

namespace UserManagement.MediatR.Handlers
{
    public class DeletePageCommandHandler : IRequestHandler<DeletePageCommand, ServiceResponse<PageDto>>
    {
        private readonly IPageRepository _pageRepository;
        private readonly IUnitOfWork<UserContext> _uow;
        public DeletePageCommandHandler(
           IPageRepository pageRepository,
            IUnitOfWork<UserContext> uow
            )
        {
            _pageRepository = pageRepository;
            _uow = uow;
        }

        public async Task<ServiceResponse<PageDto>> Handle(DeletePageCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _pageRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                return ServiceResponse<PageDto>.Return404();
            }
            _pageRepository.Delete(request.Id);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<PageDto>.Return500();
            }
            return ServiceResponse<PageDto>.ReturnSuccess();
        }
    }
}
