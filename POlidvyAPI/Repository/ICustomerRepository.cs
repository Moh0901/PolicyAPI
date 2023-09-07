using POlidvyAPI.Model;
using POlidvyAPI.ViewModels;

namespace POlidvyAPI.Repository
{
    public interface ICustomerRepository
    {
        List<CustomerTbl> GetAllCsutomers();

        CustomerTbl GetCustomerById(int id);   
        
        CustomerTbl AddNewCustomers(CustomerViewModel customerViewModel);
    }
}
