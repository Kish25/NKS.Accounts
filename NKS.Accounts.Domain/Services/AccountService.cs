using System;
using System.Threading.Tasks;
using NKS.Accounts.Core.Interfaces;
using NKS.Accounts.Domain.Models;

namespace NKS.Accounts.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<bool> CreateAsync(Account account)
        {
              return await _accountRepository.CreateAsync(account);
        }

        public async Task<bool> UpdateAsync(Account account)
        {
            return await _accountRepository.UpdateAsync(account);
        }

        public async Task<Account> GetByIdAsync(Guid id)
        {
            return await _accountRepository.GetAsync(id);
        }
    }
}