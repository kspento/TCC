using UserManagement.MediatR.Commands;
using UserManagement.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;
using Microsoft.Extensions.Logging;
using UserManagement.Data.Context;
using UserManagement.Data.Dto.Action;
using UserManagement.Data.UnitOfWork;

namespace UserManagement.MediatR.Handlers
{
    public class DeleteActionCommandHandler : IRequestHandler<DeleteActionCommand, ServiceResponse<ActionDto>>
    {
        private readonly IActionRepository _actionRepository;
        private readonly IUnitOfWork<UserContext> _uow;
        private readonly ILogger<DeleteActionCommandHandler> _logger;
        public DeleteActionCommandHandler(
           IActionRepository actionRepository,
            IUnitOfWork<UserContext> uow,
            ILogger<DeleteActionCommandHandler> logger
            )
        {
            _actionRepository = actionRepository;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<ActionDto>> Handle(DeleteActionCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _actionRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("Not found");
                return ServiceResponse<ActionDto>.Return404();
            }

            _actionRepository.Delete(request.Id);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<ActionDto>.Return500();
            }

            return ServiceResponse<ActionDto>.ReturnSuccess();
        }
    }
}
