using NKS.Accounts.Domain.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace NKS.Accounts.Core.Interfaces
{
    public interface IAccountService
    {
        Task<bool> CreateAsync(Account account);
        Task<bool> UpdateAsync(Account account);
        Task<Account> GetByIdAsync(Guid id);
    }
}