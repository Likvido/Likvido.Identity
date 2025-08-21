using System;
using System.Text.Json.Serialization;
using Likvido.Domain.Enums;

namespace Likvido.Identity.Claims
{
    public class CreditorClaimData
    {
        public int Id { get; set; }
        public string OfficeEmail { get; set; }
        public string OfficePhone { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string CountryAplha2Code { get; set; }
        public bool Settings_CanCreateCreditors { get; set; }
        public DateTime DateCreated { get; set; }

        [JsonIgnore]
        public string FullAddress => $"{Address}, {ZipCode} {City}";

        [JsonIgnore]
        public string DisplayName => CompanyName;

        public bool HasIntegration { get; set; }
        public AccountSystemType? AccountSystemType { get; set; }
    }
}
