using System;
using System.Collections.Generic;
using NKS.Accounts.Domain.Models;
using System.Threading.Tasks;

namespace NKS.Accounts.Core.Interfaces
{
    public interface IPhoneRepository
    {
        Task<bool> CreateAsync(PhoneNumber phone);
        Task<bool> AssignToAccountAsync(Guid phoneId,Guid acountId);
        Task<bool> DeleteAsync(Guid id);
        Task<PhoneNumber> GetAsync(Guid id);
        Task<List<PhoneNumber>> GetByAccountAsync(Guid accountId);
    }
}