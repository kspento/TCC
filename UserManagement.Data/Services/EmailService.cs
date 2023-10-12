using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using UserManagement.Data.Context;
using UserManagement.Data.Dto.Email;
using UserManagement.Data.Entities;
using UserManagement.Data.UnitOfWork;
using UserManagement.Domain.Model.Email;
using UserManagement.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using UserManagement.Data.Dto.User;
using System.Collections.Generic;
using System.Linq;
using UserManagement.Data.Repository.Contracts;

namespace UserManagement.Domain.Services
{
    public class EmailService
    {
        private readonly IEmailSMTPSettingRepository _emailSMTPSettingRepository;
        private readonly IUnitOfWork<UserContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<EmailSettingModel> _logger;
        private readonly UserInfoToken _userInfoToken;
        public EmailService(IEmailSMTPSettingRepository emailSMTPSettingRepository,
            IMapper mapper,
            IUnitOfWork<UserContext> uow,
            ILogger<EmailSettingModel> logger,
            UserInfoToken userInfoToken)
        {
            _emailSMTPSettingRepository = emailSMTPSettingRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
            _userInfoToken = userInfoToken;
        }

        public async Task<EmailSMTPSettingDto> AddEmailSMTPSetting(EmailSettingModel request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<EmailSMTPSetting>(request);
            _emailSMTPSettingRepository.Add(entity);

            // remove other as default
            if (entity.IsDefault)
            {
                var defaultEmailSMTPSettings = await _emailSMTPSettingRepository.All.Where(c => c.IsDefault).ToListAsync();
                defaultEmailSMTPSettings.ForEach(c => c.IsDefault = false);
                _emailSMTPSettingRepository.UpdateRange(defaultEmailSMTPSettings);
            }
            if (await _uow.SaveAsync() <= 0)
            {
                //return ServiceResponse<EmailSMTPSettingDto>.Return500();
            }
            var entityDto = _mapper.Map<EmailSMTPSettingDto>(entity);

            return entityDto;
            //return ServiceResponse<EmailSMTPSettingDto>.ReturnResultWith200(entityDto);
        }

        public async Task<EmailSMTPSettingDto> UpdateEmailSMTPSetting(EmailSettingModel request, CancellationToken cancellationToken)
        {
            var entityExist = await _emailSMTPSettingRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("Email SMTP setting does not exist.");
                //return ServiceResponse<EmailSMTPSettingDto>.Return409("Email SMTP setting does not exist.");
            }
            entityExist.IsDefault = request.IsDefault;
            entityExist.IsEnableSSL = request.IsEnableSSL;
            entityExist.Host = request.Host;
            entityExist.Port = request.Port;
            entityExist.UserName = request.UserName;
            entityExist.Password = request.Password;
            _emailSMTPSettingRepository.Update(entityExist);

            // remove other as default
            if (entityExist.IsDefault)
            {
                var defaultEmailSMTPSettings = await _emailSMTPSettingRepository.All.Where(c => c.Id != request.Id && c.IsDefault).ToListAsync();
                defaultEmailSMTPSettings.ForEach(c => c.IsDefault = false);
                _emailSMTPSettingRepository.UpdateRange(defaultEmailSMTPSettings);
            }
            if (await _uow.SaveAsync() <= 0)
            {
                //return ServiceResponse<EmailSMTPSettingDto>.Return500();
            }
            var entityDto = _mapper.Map<EmailSMTPSettingDto>(entityExist);

            return entityDto;
            
            //return ServiceResponse<EmailSMTPSettingDto>.ReturnResultWith200(entityDto);
        }

        public async Task DeleteEmailSMTPSetting(EmailSettingModel request, CancellationToken cancellationToken)
        {
            var entityExist = await _emailSMTPSettingRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("Not found");
                //return ServiceResponse<EmailSMTPSettingDto>.Return404();
            }
            if (entityExist.IsDefault)
            {
                //return ServiceResponse<EmailSMTPSettingDto>.Return422("You can not delete default Setting.");
            }
            entityExist.IsDeleted = true;
            _emailSMTPSettingRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                //return ServiceResponse<EmailSMTPSettingDto>.Return500();
            }
            //return ServiceResponse<EmailSMTPSettingDto>.ReturnResultWith204();
        }

        public async Task<EmailDto> SendEmail(SendEmailModel request, CancellationToken cancellationToken)
        {
            var defaultSmtp = await _emailSMTPSettingRepository.FindBy(c => c.IsDefault).FirstOrDefaultAsync();
            if (defaultSmtp == null)
            {
                _logger.LogError("Default SMTP setting does not exist.");
                //return ServiceResponse<EmailDto>.Return404("Default SMTP setting does not exist.");
            }
            try
            {
                EmailHelper.SendEmail(new SendEmailSpecification
                {
                    Body = request.Body,
                    FromAddress = _userInfoToken.Email,
                    Host = defaultSmtp.Host,
                    IsEnableSSL = defaultSmtp.IsEnableSSL,
                    Password = defaultSmtp.Password,
                    Port = defaultSmtp.Port,
                    Subject = request.Subject,
                    ToAddress = request.ToAddress,
                    CCAddress = request.CCAddress,
                    UserName = defaultSmtp.UserName,
                    Attachments = request.Attachments
                });

                return _mapper.Map<EmailDto>(request); 
                //return ServiceResponse<EmailDto>.ReturnSuccess();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);

                return null;
                //return ServiceResponse<EmailDto>.ReturnFailed(500, e.Message);
            }
        }

        public async Task<List<EmailSMTPSettingDto>> GetEmailSMTPSettings(EmailSettingModel request, CancellationToken cancellationToken)
        {
            var entities = await _emailSMTPSettingRepository.All.ToListAsync();
            return _mapper.Map<List<EmailSMTPSettingDto>>(entities);
        }

        public async Task<ServiceResponse<EmailSMTPSettingDto>> GetEmailSMTPSetting(EmailSettingModel request, CancellationToken cancellationToken)
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
