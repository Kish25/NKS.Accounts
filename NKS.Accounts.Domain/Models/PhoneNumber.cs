using System;

namespace NKS.Accounts.Domain.Models
{
    public class PhoneNumber
    {
        public Guid Id { get; set; }
        public Guid? AccountId { get; private set; }
        public string Number { get; set; }

        public bool SetToAccount(Guid accountId)
        {
            if (AccountId != null)
                return false;

            AccountId = accountId;

            return true;
        }
    }
}