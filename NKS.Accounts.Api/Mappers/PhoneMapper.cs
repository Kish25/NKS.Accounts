using NKS.Accounts.Domain.Models;
using System;
using System.Collections.Generic;
using NKS.Accounts.Api.ApiModels;

namespace NKS.Accounts.Api.Mappers
{

    public interface IPhoneNumberMapper
    {
        PhoneNumber Map(string phoneNumber);
        AccountNumbers Map(Account account, List<PhoneNumber> numbers);
    }

    public class PhoneNumberMapper : IPhoneNumberMapper
    {
        public PhoneNumber Map(string phoneNumber)
        {
            PhoneNumber number = new PhoneNumber()
            {
                Id = Guid.NewGuid(),
                Number = phoneNumber,
            };
            return number;
        }

        public AccountNumbers Map(Account account, List<PhoneNumber> numbers)
        {
            return new AccountNumbers()
            {
                Accountid = account.Id,
                Name = account.Name,
                Numbers = numbers
            };
        }
    }

}