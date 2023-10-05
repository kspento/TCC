﻿using UserManagement.Repository;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Data.Repository.UserRepository;
using UserManagement.Data.Repository.UserClaimRepository;
using UserManagement.Data.UnitOfWork;
using UserManagement.Data.Repository.UserRole;
using UserManagement.Data.Repository.LoginAudit;
using UserManagement.Data.PropertyMapping;
using UserManagement.Data.Repository.RoleClaim;
using UserManagement.Data.Repository.PageAction;

namespace UserManagement.Api.Helpers
{
    public static class DependencyInjectionExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped<IPropertyMappingService, PropertyMappingService>();
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<IActionRepository, ActionRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserClaimRepository, UserClaimRepository>();
            services.AddScoped<IRoleClaimRepository, RoleClaimRepository>();
            services.AddScoped<IPageActionRepository, PageActionRepository>();
            services.AddScoped<ILoginAuditRepository, LoginAuditRepository>();
            services.AddScoped<IUserAllowedIPRepository, UserAllowedIPRepository>();
            services.AddScoped<IAppSettingRepository, AppSettingRepository>();
            services.AddScoped<INLogRespository, NLogRespository>();
            services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
            services.AddScoped<IEmailSMTPSettingRepository, EmailSMTPSettingRepository>();
        }
    }
}
