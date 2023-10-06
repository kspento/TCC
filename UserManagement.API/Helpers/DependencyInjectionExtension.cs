using Microsoft.Extensions.DependencyInjection;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Data.Repository.UserClaimRepository;
using UserManagement.Data.UnitOfWork;
using UserManagement.Data.Repository.LoginAudit;
using UserManagement.Data.PropertyMapping;
using UserManagement.Data.Repository.RoleClaim;
using UserManagement.Data.Repository.PageAction;
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
            services.AddScoped<Data.Repository.Contracts.IUserRepository, Data.Repository.UserRepository.UserRepository>();
            services.AddScoped<Data.Repository.Contracts.IUserRoleRepository, Data.Repository.UserRole.UserRoleRepository>();
            services.AddScoped<IUserClaimRepository, UserClaimRepository>();
            services.AddScoped<IRoleClaimRepository, RoleClaimRepository>();
            services.AddScoped<IPageActionRepository, PageActionRepository>();
            services.AddScoped<ILoginAuditRepository, LoginAuditRepository>();
            services.AddScoped<Data.Repository.Contracts.IUserAllowedIPRepository, Data.Repository.UserRepository.UserAllowedIPRepository>();
            services.AddScoped<IAppSettingRepository, AppSettingRepository>();
            services.AddScoped<INLogRespository, NLogRespository>();
            services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
            services.AddScoped<IEmailSMTPSettingRepository, EmailSMTPSettingRepository>();
        }
    }
}
