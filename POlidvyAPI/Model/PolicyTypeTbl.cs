using System;
using System.Collections.Generic;
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
        public string? PolicyTypeName { get; set; }
        public string? PolicyTypeCode { get; set; }

        [JsonIgnore]
        public virtual ICollection<PolicyTbl> PolicyTbls { get; set; }
    }
}
