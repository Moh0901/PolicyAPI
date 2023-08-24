using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace POlidvyAPI.Model.ViewModel
{
    public class SearchViewModel
    {
        public int PolicyId { get; set; }
        public string PolicyName { get; set; } = null!;

        public int PolicyTypeId { get; set; }

        public string PolicyCompany { get; set; } = null!;

        [DisplayName("Number of years")]
        public int PolicyDuration { get; set; }
    }
}
