using UserManagement.Data.Dto.NLog;

namespace UserManagement.Domain.Model.NLog
{
    public class AddLogModel : NLogDto
    {
        public string ErrorMessage { get; set; }
        public string Stack { get; set; }
    }
}
