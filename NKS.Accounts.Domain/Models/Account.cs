using System;

namespace NKS.Accounts.Domain.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; private set; }

        public void SetStatus(string status)
        {
            Status = status;
        }
    }
}