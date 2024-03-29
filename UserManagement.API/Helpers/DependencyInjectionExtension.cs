﻿using Microsoft.Extensions.DependencyInjection;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Data.Repository.UserClaimRepository;
using UserManagement.Data.UnitOfWork;
using UserManagement.Data.Repository.LoginAudit;
using UserManagement.Data.PropertyMapping;
using UserManagement.Data.Repository.RoleClaim;
using UserManagement.Data.Repository.PageAction;
using UserManagement.Data.Repository.Email;
using UserManagement.Data.Repository.NLog;
using UserManagement.Data.Repository.Action;
using UserManagement.Data.Repository.Role;
using UserManagement.Data.Repository.AppSetting;
using UserManagement.Data.Repository.EmailTemplate;
using UserManagement.Repository;
using UserManagement.Domain.Contracts.Services;
using UserManagement.Domain.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using UserManagement.API.Filters;

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
            services.AddScoped<IUserRoleRepository, Data.Repository.UserRole.UserRoleRepository>();
            services.AddScoped<IUserClaimRepository, UserClaimRepository>();
            services.AddScoped<IRoleClaimRepository, RoleClaimRepository>();
            services.AddScoped<IPageActionRepository, PageActionRepository>();
            services.AddScoped<ILoginAuditRepository, LoginAuditRepository>();
            services.AddScoped<IUserAllowedIPRepository, UserAllowedIPRepository>();
            services.AddScoped<IAppSettingRepository, AppSettingRepository>();
            services.AddScoped<INLogRespository, NLogRespository>();
            services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
            services.AddScoped<IEmailSMTPSettingRepository, EmailSMTPSettingRepository>();
            services.AddScoped<IActionService, ActionService>();
            services.AddScoped<IAppSettingService, AppSettingService>();
            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEmailTemplateService, EmailTemplateService>();
            services.AddScoped<ILoginAuditService, LoginAuditService>();
            services.AddScoped<INLogService, NLogService>();
            services.AddScoped<IPageActionService, PageActionService>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISocialLoginService, SocialLoginService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<CustomExceptionFilter>();

            services.AddScoped<IUserService, UserService>();
        }
    }
}
