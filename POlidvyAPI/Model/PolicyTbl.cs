using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace POlidvyAPI.Model
{
    public partial class PolicyTbl
    {
        public int PolicyId { get; set; }
        public string PolicyCode { get; set; } = null!;
        public string PolicyName { get; set; } = null!;
        public DateTime PolicyStartDate { get; set; }
        public string PolicyCompany { get; set; } = null!;
        public int PolicyDuration { get; set; }
        public string PolicyInitialDeposit { get; set; } = null!;
        public int PolicyTypeId { get; set; }
        public int UserTypeId { get; set; }
        public string PolicyTermsPerYear { get; set; } = null!;
        public string PolicyAmount { get; set; } = null!;
        public string PolicyInterest { get; set; } = null!;

        public virtual PolicyTypeTbl PolicyType { get; set; } = null!;

        [JsonIgnore]
        public virtual UserTypeTbl UserType { get; set; } = null!;
    }
}
