using System.Threading.Tasks;
using NKS.Accounts.Core.Interfaces;

namespace NKS.Accounts.Core.Services
{
    public class UserService : IUserService
    {
        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            return username.Equals("me") && password.Equals("Pa55");
        }
    }
}