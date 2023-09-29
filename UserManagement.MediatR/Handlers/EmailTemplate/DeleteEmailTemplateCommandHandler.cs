using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Context;
using UserManagement.Data.Dto.User;
using UserManagement.Data.UnitOfWork;
using UserManagement.Helper;
using UserManagement.MediatR.Commands;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class DeleteEmailTemplateCommandHandler : IRequestHandler<DeleteEmailTemplateCommand, ServiceResponse<bool>>
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IUnitOfWork<UserContext> _uow;
        private readonly UserInfoToken _userInfoToken;
        private readonly ILogger<UpdateAppSettingCommandHandler> _logger;
        public DeleteEmailTemplateCommandHandler(
           IEmailTemplateRepository emailTemplateRepository,
            IUnitOfWork<UserContext> uow,
            UserInfoToken userInfoToken,
            ILogger<UpdateAppSettingCommandHandler> logger
            )
        {
            _emailTemplateRepository = emailTemplateRepository;
            _uow = uow;
            _userInfoToken = userInfoToken;
            _logger = logger;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteEmailTemplateCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _emailTemplateRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("Email Template Not Found.");
                return ServiceResponse<bool>.Return404();
            }
            entityExist.IsDeleted = true;
            _emailTemplateRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<bool>.Return500();
            }
            return ServiceResponse<bool>.ReturnResultWith204();
        }
    }
}
