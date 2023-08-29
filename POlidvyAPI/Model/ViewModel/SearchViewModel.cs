using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace POlidvyAPI.Model.ViewModel
{
    public class SearchViewModel
    {
        public int PolicyId { get; set; }
        public string? PolicyName { get; set; } 

        public string? PolicyTypeName { get; set; }

        public string? PolicyCompany { get; set; } 

        [DisplayName("Number of years")]
        public int PolicyDuration { get; set; }
    }
}
