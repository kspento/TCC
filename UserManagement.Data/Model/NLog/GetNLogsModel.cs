using System;
using UserManagement.Data.Repository.NLog;
using UserManagement.Data.Resources;

namespace UserManagement.Domain.Model.NLog
{
    public class GetNLogsModel : NLogList
    {
        public NLogResource NLogResource { get; set; }

        public Guid Id { get; set; }
    }
}
