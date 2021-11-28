using System;

namespace NKS.Accounts.Api.Configuration
{
    public class ApiInfo
    {
        public string Version { get; set; }
        public DateTime CreationDate { get; set; }
        public Swagger ApiVersionInfo { get; set; }

    }
}