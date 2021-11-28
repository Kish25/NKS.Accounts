using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NKS.Accounts.Api.Requests
{
    public class CreateAccount
    {
        [Required]
        public string Name { get; set; }

    }
}
