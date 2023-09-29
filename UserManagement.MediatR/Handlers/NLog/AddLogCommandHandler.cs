using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data;
using UserManagement.Data.Dto;
using UserManagement.Domain;
using UserManagement.Helper;
using UserManagement.MediatR.Commands;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class AddLogCommandHandler : IRequestHandler<AddLogCommand, ServiceResponse<NLogDto>>
    {
        private readonly INLogRespository _nLogRespository;
        private readonly IUnitOfWork<UserContext> _uow;
        public AddLogCommandHandler(
           INLogRespository nLogRespository,
            IUnitOfWork<UserContext> uow
            )
        {
            _nLogRespository = nLogRespository;
            _uow = uow;
        }
        public async Task<ServiceResponse<NLogDto>> Handle(AddLogCommand request, CancellationToken cancellationToken)
        {
            _nLogRespository.Add(new NLog
            {
                Id = Guid.NewGuid(),
                Logged = DateTime.Now.ToLocalTime(),
                Level = "Error",
                Message = request.ErrorMessage,
                Source = "Angular",
                Exception = request.Stack
            });
            await _uow.SaveAsync();
            return ServiceResponse<NLogDto>.ReturnSuccess();
        }
    }
}
