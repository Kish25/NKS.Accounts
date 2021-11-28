using System;
using NKS.Accounts.Domain.Models;

namespace NKS.Accounts.Api.Mappers
{
    public interface IAccountRequestMapper
    {
        Account Map(string accountName);
    }

    public class AccountRequestMapper: IAccountRequestMapper
    {
        public Account Map(string accountName)
        {
            Account account = new Account()
            {
                Id = Guid.NewGuid(),
                Name = accountName
            };
            account.SetStatus("Active");
            return account;
        }
    }
}