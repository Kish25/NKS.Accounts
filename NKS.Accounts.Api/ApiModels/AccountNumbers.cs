using System;
using System.Collections.Generic;
using System.Net;
using NKS.Accounts.Domain.Models;

namespace NKS.Accounts.Api.ApiModels
{
    public class AccountNumbers
    {
        public Guid Accountid { get; set; }
        public string Name { get; set; }
        public List<PhoneNumber> Numbers { get; set; }
    }

}