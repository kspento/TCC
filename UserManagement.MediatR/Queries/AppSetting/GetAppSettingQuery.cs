﻿using MediatR;
using System;
using UserManagement.Data.Dto.AppSetting;
using UserManagement.Helper;

namespace UserManagement.MediatR.Queries
{
    public class GetAppSettingQuery : IRequest<ServiceResponse<AppSettingDto>>
    {
        public Guid Id { get; set; }
    }
}
