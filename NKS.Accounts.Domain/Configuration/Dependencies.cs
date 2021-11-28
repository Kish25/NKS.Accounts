using Microsoft.Extensions.DependencyInjection;
using NKS.Accounts.Core.Interfaces;
using NKS.Accounts.Core.Services;

namespace NKS.Accounts.Core.Configuration
{
    public static class Dependencies
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IUserService, UserService>()
                .AddTransient<IPhoneService, PhoneService>()
                .AddTransient<IAccountService, AccountService>();
        }
    }
}