using System.ComponentModel.DataAnnotations;

namespace NKS.Accounts.Api.Requests
{
    public class CreatePhoneNumber
    {
        [Required]
        public string Number { get; set; }

    }
}
