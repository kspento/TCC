﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Data.Context;
using UserManagement.Data.Dto.LoginAudit;
using UserManagement.Data.Entities;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.PropertyMapping;
using UserManagement.Data.Resources;
using UserManagement.Data.UnitOfWork;

namespace UserManagement.Repository
{
    public class LoginAuditRepository : GenericRepository<LoginAudit, UserContext>,
       ILoginAuditRepository
    {
        private readonly IUnitOfWork<UserContext> _uow;
        private readonly ILogger<LoginAuditRepository> _logger;
        private readonly IPropertyMappingService _propertyMappingService;
        public LoginAuditRepository(
            IUnitOfWork<UserContext> uow,
            ILogger<LoginAuditRepository> logger,
            IPropertyMappingService propertyMappingService
            ) : base(uow)
        {
            _uow = uow;
            _logger = logger;
            _propertyMappingService = propertyMappingService;
        }

        public async Task<LoginAuditList> GetDocumentAuditTrails(LoginAuditResource loginAuditResrouce)
        {
            var collectionBeforePaging = All;
            collectionBeforePaging =
               collectionBeforePaging.ApplySort(loginAuditResrouce.OrderBy,
               _propertyMappingService.GetPropertyMapping<LoginAuditDto, LoginAudit>());

            if (!string.IsNullOrWhiteSpace(loginAuditResrouce.UserName))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(c => EF.Functions.Like(c.UserName, $"%{loginAuditResrouce.UserName}%"));
            }

            var loginAudits = new LoginAuditList();
            return await loginAudits.Create(
                collectionBeforePaging,
                loginAuditResrouce.Skip,
                loginAuditResrouce.PageSize
                );
        }

        public async Task LoginAudit(LoginAuditDto loginAudit)
        {
            try
            {
                Add(new LoginAudit
                {
                    Id = Guid.NewGuid(),
                    LoginTime = DateTime.Now.ToLocalTime(),
                    Provider = loginAudit.Provider,
                    Status = loginAudit.Status,
                    UserName = loginAudit.UserName,
                    RemoteIP = loginAudit.RemoteIP,
                    Latitude = loginAudit.Latitude,
                    Longitude = loginAudit.Longitude
                });
                await _uow.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
