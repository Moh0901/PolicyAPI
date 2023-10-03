using System.Text.Json.Serialization;

namespace POlidvyAPI.Model
{
    public partial class PolicyTbl
    {
        public int PolicyId { get; set; }
        public string? PolicyCode { get; set; }
        public string? PolicyName { get; set; }
        public DateTime PolicyStartDate { get; set; }
        public string? PolicyCompany { get; set; }
        public int? PolicyDuration { get; set; }
        public decimal? PolicyInitialDeposit { get; set; }
        public int? PolicyTypeId { get; set; }
        public int? UserTypeId { get; set; }
        public int? PolicyTermsPerYear { get; set; }
        public decimal? PolicyAmount { get; set; }
        public decimal? PolicyInterest { get; set; }

        public virtual PolicyTypeTbl? PolicyType { get; set; }
        [JsonIgnore]
        public virtual UserTypeTbl? UserType { get; set; }
    }
}
