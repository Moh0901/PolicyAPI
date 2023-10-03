using Microsoft.Build.Framework;

namespace POlidvyAPI.ViewModels
{
    public class CustomerViewModel
    {

        [Required]
        public string? CustomerFirstName { get; set; }
        public string? CustomerLastName { get; set; }
        public DateTime? CustomerBirthDate { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerContactNo { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPanNo { get; set; }
        public decimal? CustomerSalary { get; set; }
        public int? EmployerTypeId { get; set; }
        public string? EmployerName { get; set; }
    }
}
