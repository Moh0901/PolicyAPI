using System.Text.Json.Serialization;

namespace POlidvyAPI.Model
{
    public partial class PolicyTypeTbl
    {
        public PolicyTypeTbl()
        {
            PolicyTbls = new HashSet<PolicyTbl>();
        }

        public int PolicyTypeId { get; set; }
        public string PolicyTypeName { get; set; } = null!;
        public string PolicyTypeCode { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<PolicyTbl> PolicyTbls { get; set; }
    }
}
