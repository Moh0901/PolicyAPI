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
        public string UserTypeName { get; set; } = null!;
        public string UserIncome { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<PolicyTbl> PolicyTbls { get; set; }
    }
}
