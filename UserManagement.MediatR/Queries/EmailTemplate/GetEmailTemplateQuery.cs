﻿using MediatR;
using System;
using UserManagement.Data.Dto.EmailTemplate;
using UserManagement.Helper;

namespace UserManagement.MediatR.Queries
{
    public class GetEmailTemplateQuery : IRequest<ServiceResponse<EmailTemplateDto>>
    {
        public Guid Id { get; set; }
    }
}
