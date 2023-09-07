using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using POlidvyAPI.Model;
using POlidvyAPI.ViewModels;


namespace POlidvyAPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly PolicyMDBContext _context;

        public CustomerRepository(PolicyMDBContext context)
        {
            _context = context;
        }

        public CustomerTbl AddNewCustomers(CustomerViewModel customerViewModel)
        {
            CustomerTbl newCustomer = new CustomerTbl();

                newCustomer.CustomerAddress = customerViewModel.CustomerAddress;
                newCustomer.CustomerFirstName = customerViewModel.CustomerFirstName;
                newCustomer.CustomerLastName = customerViewModel.CustomerLastName;
                newCustomer.CustomerEmail = customerViewModel.CustomerEmail;
                newCustomer.CustomerBirthDate = customerViewModel.CustomerBirthDate;
                newCustomer.CustomerContactNo = customerViewModel.CustomerContactNo;
                newCustomer.CustomerSalary = customerViewModel.CustomerSalary;
                newCustomer.EmployerTypeId = customerViewModel.EmployerTypeId;
                newCustomer.EmployerName = customerViewModel.EmployerName;
                newCustomer.CustomerPanNo = customerViewModel.CustomerPanNo;

                _context.CustomerTbls.Add(newCustomer);

                _context.SaveChanges();
            
            return newCustomer;
        }

        public List<CustomerTbl> GetAllCsutomers()
        {
            var customerList =  _context.CustomerTbls.Include(x=>x.EmployerType).ToList();

            return customerList;
        }

        public CustomerTbl GetCustomerById(int id)
        {
            var customer =  _context.CustomerTbls.Include(x => x.EmployerType).FirstOrDefault(y => y.CustomerId == id);

            return customer;
        }
    }
}
