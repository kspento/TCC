﻿using MediatR;
using System.Collections.Generic;
using UserManagement.Data.Dto.EmailTemplate;
using UserManagement.Helper;

namespace UserManagement.MediatR.Queries
{
    public class GetAllEmailTemplateQuery : IRequest<ServiceResponse<List<EmailTemplateDto>>>
    {

    }
}
