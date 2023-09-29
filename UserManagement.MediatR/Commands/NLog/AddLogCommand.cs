using MediatR;
using UserManagement.Data.Dto.NLog;
using UserManagement.Helper;

namespace UserManagement.MediatR.Commands
{
    public class AddLogCommand : IRequest<ServiceResponse<NLogDto>>
    {
        public string ErrorMessage { get; set; }
        public string Stack { get; set; }
    }
}
