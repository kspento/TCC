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
using UserManagement.Domain.Exception;

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
            _emailTemplateRepository = emailTemplateRepository;
            _mapper = mapper;
            _uow = uow;
            _userInfoToken = userInfoToken;
            _logger = logger;
        }

        public async Task<EmailTemplateDto> AddEmailTemplate(AddEmailTemplateModel request)
        {
            var entityExist = await _emailTemplateRepository.FindBy(c => c.Name == request.Name).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("Email Template already exist.");
                throw new AlreadyExistsException("Email Template already exist.");
            }
            var entity = _mapper.Map<EmailTemplate>(request);
            entity.Id = Guid.NewGuid();
            entity.CreatedBy = Guid.Parse(_userInfoToken.Id);
            entity.CreatedDate = DateTime.Now.ToLocalTime();
            entity.ModifiedBy = Guid.Parse(_userInfoToken.Id);
            _emailTemplateRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                throw new System.Exception();
            }
            var entityDto = _mapper.Map<EmailTemplateDto>(entity);

            return entityDto;
        }

        public async Task<bool> DeleteEmailTemplate(DeleteEmailTemplateModel request)
        {
            var entityExist = await _emailTemplateRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("Email Template Not Found.");
                throw new NotFoundException(string.Empty);
            }
            entityExist.IsDeleted = true;
            _emailTemplateRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                throw new System.Exception();
            }
            return true;
        }

        public async Task<List<EmailTemplateDto>> GetAllEmailTemplate()
        {
            var entities = await _emailTemplateRepository.All.ToListAsync();

            return _mapper.Map<List<EmailTemplateDto>>(entities);
        }

        public async Task<EmailTemplateDto> UpdateEmailTemplate(UpdateEmailTemplateModel request)
        {
            var entityExist = await _emailTemplateRepository.FindBy(c => c.Name == request.Name && c.Id != request.Id).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("Email Template already exist.");

                throw new AlreadyExistsException("Email Template already exist.");
            }
            var entity = _mapper.Map<EmailTemplate>(request);
            entity.ModifiedBy = Guid.Parse(_userInfoToken.Id);
            _emailTemplateRepository.Update(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                throw new System.Exception();
            }
            var entityDto = _mapper.Map<EmailTemplateDto>(entity);

            return entityDto;
        }
        public async Task<EmailTemplateDto> GetEmailTemplate(GetEmailTemplateModel request)
        {
            var emailTemplate = await _emailTemplateRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            if (emailTemplate == null)
            {
                _logger.LogError("Email Template is not available");
                throw new NotFoundException(string.Empty);
            }

            return _mapper.Map<EmailTemplateDto>(emailTemplate);            
        }
    }
}
