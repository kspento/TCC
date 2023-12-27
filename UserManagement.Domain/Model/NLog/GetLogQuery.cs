using System;
using UserManagement.Data.Dto.NLog;

namespace UserManagement.Domain.Model.NLog
{
    public class GetLogQuery : NLogDto
    {
        public Guid Id { get; set; }
    }
}
