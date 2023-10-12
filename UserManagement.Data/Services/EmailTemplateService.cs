using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using UserManagement.Data.Context;
using UserManagement.Data.Dto.EmailTemplate;
using UserManagement.Data.Dto.User;
using UserManagement.Data.Entities;
using UserManagement.Data.UnitOfWork;
using System.Collections.Generic;
using UserManagement.Domain.Model.EmailTemplate;
using UserManagement.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Contracts.Services;

namespace UserManagement.Domain.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IUnitOfWork<UserContext> _uow;
        private readonly IMapper _mapper;
        private readonly UserInfoToken _userInfoToken;
        private readonly ILogger<AddEmailTemplateModel> _logger;
        public EmailTemplateService(IEmailTemplateRepository emailTemplateRepository,
            IMapper mapper,
            IUnitOfWork<UserContext> uow,
            UserInfoToken userInfoToken,
            ILogger<AddEmailTemplateModel> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _userInfoToken = userInfoToken;
            _logger = logger;
        }

        public async Task<EmailTemplateDto> AddEmailTemplate(AddEmailTemplateModel request, CancellationToken cancellationToken)
        {
            var entityExist = await _emailTemplateRepository.FindBy(c => c.Name == request.Name).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("Email Template already exist.");
                //return ServiceResponse<EmailTemplateDto>.Return409("Email Template already exist.");
            }
            var entity = _mapper.Map<EmailTemplate>(request);
            entity.Id = Guid.NewGuid();
            entity.CreatedBy = Guid.Parse(_userInfoToken.Id);
            entity.CreatedDate = DateTime.Now.ToLocalTime();
            entity.ModifiedBy = Guid.Parse(_userInfoToken.Id);
            _emailTemplateRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                //return ServiceResponse<EmailTemplateDto>.Return500();
            }
            var entityDto = _mapper.Map<EmailTemplateDto>(entity);

            return entityDto;
            //return ServiceResponse<EmailTemplateDto>.ReturnResultWith200(entityDto);
        }

        public async Task<bool> DeleteEmailTemplate(DeleteEmailTemplateModel request, CancellationToken cancellationToken)
        {
            var entityExist = await _emailTemplateRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("Email Template Not Found.");
                //return ServiceResponse<bool>.Return404();
            }
            entityExist.IsDeleted = true;
            _emailTemplateRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                //return ServiceResponse<bool>.Return500();
            }
            return true;
        }

        public async Task<List<EmailTemplateDto>> GetAllEmailTemplate(CancellationToken cancellationToken)
        {
            var entities = await _emailTemplateRepository.All.ToListAsync();

            return _mapper.Map<List<EmailTemplateDto>>(entities);
            //return ServiceResponse<List<EmailTemplateDto>>.ReturnResultWith200(_mapper.Map<List<EmailTemplateDto>>(entities));
        }

        public async Task<EmailTemplateDto> UpdateEmailTemplate(UpdateEmailTemplateModel request, CancellationToken cancellationToken)
        {
            var entityExist = await _emailTemplateRepository.FindBy(c => c.Name == request.Name && c.Id != request.Id).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("Email Template already exist.");

                //return ServiceResponse<EmailTemplateDto>.Return409("Email Template already exist.");
            }
            var entity = _mapper.Map<EmailTemplate>(request);
            entity.ModifiedBy = Guid.Parse(_userInfoToken.Id);
            _emailTemplateRepository.Update(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                //return ServiceResponse<EmailTemplateDto>.Return500();
            }
            var entityDto = _mapper.Map<EmailTemplateDto>(entity);

            return entityDto;
            //return ServiceResponse<EmailTemplateDto>.ReturnResultWith200(entityDto);
        }
        public async Task<EmailTemplateDto> GetEmailTemplate(GetEmailTemplateModel request, CancellationToken cancellationToken)
        {
            var emailTemplate = await _emailTemplateRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            if (emailTemplate == null)
            {
                _logger.LogError("Email Template is not available");
                //return ServiceResponse<EmailTemplateDto>.Return404();
            }
            return _mapper.Map<EmailTemplateDto>(emailTemplate);

            //return ServiceResponse<EmailTemplateDto>.ReturnResultWith200(_mapper.Map<EmailTemplateDto>(emailTemplate));
        }
    }
}
