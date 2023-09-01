using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POlidvyAPI.Model;
using POlidvyAPI.Model.ViewModel;

namespace POlidvyAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly PolicyDBContext _context;

        public CustomerController(PolicyDBContext context)
        {
            _context = context;
        }

        [HttpGet("/api/v1.0/customer/getall")]
        public async Task<ActionResult<IEnumerable<CustomerTbl>>> GetCustomers()
        {
            if (_context.CustomerTbls == null)
            {
                return NotFound();
            }

            var customers = await _context.CustomerTbls.Include(x => x.EmployerType).ToListAsync();

            return Ok(customers);
        }

        [HttpGet("/api/v1.0/customer/getby{id}")]

        public async Task<ActionResult<IEnumerable<CustomerTbl>>> GetCustomerById(int id)
        {
            if (_context.CustomerTbls == null)
            {
                return NotFound();
            }

            var customer = await _context.CustomerTbls.Include(x => x.EmployerType).FirstOrDefaultAsync(y => y.CustomerId == id);

            return Ok(customer);
        }


        [HttpPost("/api/v1.0/customer/register")]

        public async Task<IActionResult> AddCustomer(CustomerViewModel customerViewModel)
        {

            CustomerTbl customer = new CustomerTbl();
            if (ModelState.IsValid)
            {
                customer.CustomerId = customerViewModel.CustomerId;
                customer.CustomerAddress = customerViewModel.CustomerAddress;
                customer.CustomerFirstName = customerViewModel.CustomerFirstName;
                customer.CustomerLastName = customerViewModel.CustomerLastName;
                customer.CustomerEmail = customerViewModel.CustomerEmail;
                customer.CustomerBirthDate = customerViewModel.CustomerBirthDate;
                customer.CustomerContactNo = customerViewModel.CustomerContactNo;
                customer.CustomerSalary = customerViewModel.CustomerSalary;
                customer.EmployerTypeId = customerViewModel.EmployerTypeId;
                customer.EmployerName = customerViewModel.EmployerName;

                await _context.CustomerTbls.AddAsync(customer);

                await _context.SaveChangesAsync();
            }
            return Ok(customer);
        }
    }
}
