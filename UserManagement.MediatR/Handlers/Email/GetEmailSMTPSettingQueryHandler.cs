﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto;
using UserManagement.Helper;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class GetEmailSMTPSettingQueryHandler : IRequestHandler<GetEmailSMTPSettingQuery, ServiceResponse<EmailSMTPSettingDto>>
    {
        private readonly IEmailSMTPSettingRepository _emailSMTPSettingRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetRoleQueryHandler> _logger;

        public GetEmailSMTPSettingQueryHandler(
           IEmailSMTPSettingRepository emailSMTPSettingRepository,
            IMapper mapper,
            ILogger<GetRoleQueryHandler> logger)
        {
            _emailSMTPSettingRepository = emailSMTPSettingRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResponse<EmailSMTPSettingDto>> Handle(GetEmailSMTPSettingQuery request, CancellationToken cancellationToken)
        {
            var entity = await _emailSMTPSettingRepository.All.Where(c => c.Id == request.Id).FirstOrDefaultAsync();
            if (entity != null)
                return ServiceResponse<EmailSMTPSettingDto>.ReturnResultWith200(_mapper.Map<EmailSMTPSettingDto>(entity));
            else
            {
                _logger.LogError("Not found");
                return ServiceResponse<EmailSMTPSettingDto>.Return404();
            }
        }
    }
}
