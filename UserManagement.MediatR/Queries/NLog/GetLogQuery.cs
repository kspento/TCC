using MediatR;
using System;
using UserManagement.Data.Dto.NLog;
using UserManagement.Helper;

namespace UserManagement.MediatR.Queries
{
    public class GetLogQuery : IRequest<ServiceResponse<NLogDto>>
    {
        public Guid Id { get; set; }
    }
}
