using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POlidvyAPI.Repository;
using POlidvyAPI.ViewModels;

namespace POlidvyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        
        [HttpGet("/api/v1.0/customer/getall")]

        public IActionResult GetCustomers()
        {
            var result = _customerRepository.GetAllCsutomers();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        [HttpGet("/api/v1.0/customer/getby/{id}")]

        public IActionResult GetCustomerById(int id)
        {
            var result = _customerRepository.GetCustomerById(id);

            if(result == null)
            {
                return NotFound($"Customer Not Found of {id}.");
            }
            return Ok(result);
        }


        [HttpPost("/api/v1.0/customer/register")]

        public IActionResult PostCustomer(CustomerViewModel customerViewModel)
        {
            if(customerViewModel == null)
            {
                return NotFound("New Customer Not Added.");
            }

            var result = _customerRepository.AddNewCustomers(customerViewModel);

            return Ok(result);
        }
    }
}
