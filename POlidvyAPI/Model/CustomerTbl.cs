using System;
using System.Collections.Generic;

namespace POlidvyAPI.Model
{
    public partial class CustomerTbl
    {
        public int CustomerId { get; set; }
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

        
        public virtual EmployerTypeTbl? EmployerType { get; set; }
    }
}
