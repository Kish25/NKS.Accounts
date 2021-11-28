using System.Threading.Tasks;

namespace NKS.Accounts.Core.Interfaces
{
    public interface IUserService
    {
        Task<bool> ValidateCredentialsAsync(string username, string password);

    }
}