using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NKS.Accounts.Domain.Models;

namespace NKS.Accounts.Core.Interfaces
{
    public interface IPhoneService
    {
        Task<bool> AssignToAccountAsync(Guid accountId,Guid phoneId);
        Task<List<PhoneNumber>> GetByAccountAsync(Guid accountId);
        Task<PhoneNumber> GetByIdAsync(Guid accountId);
        Task<bool> CreateAsync(PhoneNumber phoneNumber);
        Task<bool> DeleteAsync(Guid id);
    }
}