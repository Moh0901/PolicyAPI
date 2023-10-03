using System.Text.Json.Serialization;

namespace POlidvyAPI.Model
{
    public partial class EmployerTypeTbl
    {
        public EmployerTypeTbl()
        {
            CustomerTbls = new HashSet<CustomerTbl>();
        }

        public int EmployerTypeId { get; set; }
        public string? EmployerTypeName { get; set; }

        [JsonIgnore]
        public virtual ICollection<CustomerTbl> CustomerTbls { get; set; }
    }
}
