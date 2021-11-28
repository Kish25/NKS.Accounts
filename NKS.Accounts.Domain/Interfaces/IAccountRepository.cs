using NKS.Accounts.Domain.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace NKS.Accounts.Core.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> CreateAsync(Account account);
        Task<bool> UpdateAsync(Account account);
        Task<Account> GetAsync(Guid id);
        Task<List<Account>> GetAllAsync(Guid id);

    }
}