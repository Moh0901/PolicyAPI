using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace POlidvyAPI.Model
{
    public partial class UserTypeTbl
    {
        public UserTypeTbl()
        {
            PolicyTbls = new HashSet<PolicyTbl>();
        }

        public int UserTypeId { get; set; }
        public string? UserTypeName { get; set; }
        public string? UserIncomePerYear { get; set; }

        [JsonIgnore]
        public virtual ICollection<PolicyTbl> PolicyTbls { get; set; }
    }
}
