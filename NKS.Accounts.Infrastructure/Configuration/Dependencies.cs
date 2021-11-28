using Microsoft.Extensions.DependencyInjection;
using NKS.Accounts.Core.Interfaces;
using NKS.Accounts.Infrastructure.Repositories;

namespace NKS.Accounts.Infrastructure.Configuration
{
    public static class Dependencies
    {
        public static IServiceCollection AddInfrastruture(this IServiceCollection services, string sqlConnectionString)
        {
            return services
                .AddTransient<IAccountRepository, AccountRepository>()
                .AddTransient<IPhoneRepository, PhoneRepository>()
                .AddSqlServer(sqlConnectionString);
        }
    }
}