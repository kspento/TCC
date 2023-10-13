using Microsoft.Extensions.DependencyInjection;
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
        }
    }
}
