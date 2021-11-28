using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System;
using System.Data.SqlClient;

namespace NKS.Accounts.Infrastructure.Configuration
{
    public static class MSSQLServer
    {
        public static IServiceCollection AddSqlServer(
            this IServiceCollection services,
            string connString)
        {
            return services.AddTransient(
                (Func<IServiceProvider, IDbConnection>)(sp =>
                    (IDbConnection)new SqlConnection(connString)));
        }

    }
}