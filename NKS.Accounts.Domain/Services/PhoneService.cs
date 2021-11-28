using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NKS.Accounts.Core.Interfaces;
using NKS.Accounts.Domain.Models;

namespace NKS.Accounts.Core.Services
{
    public class PhoneService : IPhoneService
    {
        private readonly IPhoneRepository _phoneRepository;

        public PhoneService(IPhoneRepository phoneRepository)
        {
            _phoneRepository = phoneRepository;
        }

        public async Task<bool> AssignToAccountAsync(Guid accountId, Guid phoneId)
        {
            return await _phoneRepository.AssignToAccountAsync(phoneId, accountId);
        }

        public async Task<List<PhoneNumber>> GetByAccountAsync(Guid accountId)
        {
            return await _phoneRepository.GetByAccountAsync(accountId);
        }

        public async Task<PhoneNumber> GetByIdAsync(Guid id)
        {
            return await _phoneRepository.GetAsync(id);
        }


        public async Task<bool> CreateAsync(PhoneNumber phoneNumber)
        {
            return await _phoneRepository.CreateAsync(phoneNumber);

        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _phoneRepository.DeleteAsync(id);
        }
    }
}