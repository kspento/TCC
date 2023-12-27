using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto.User;
using UserManagement.Domain.Model.Social;
using UserManagement.Helper;

namespace UserManagement.Domain.Contracts.Services
{
    public interface ISocialLoginService
    {
        Task<UserAuthDto> SocialLogin(SocialLoginModel request);
    }
}