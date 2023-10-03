using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POlidvyAPI.Repository;
using POlidvyAPI.ViewModels;

namespace POlidvyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyRepository _policyRepository;

        public PolicyController(IPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }
        [Authorize(Roles = "User,Admin")]
        [HttpGet("/api/v1.0/policy/getall")]
        public IActionResult GetPolicies()
        {
            var result = _policyRepository.GetAllPolicies();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("/api/v1.0/policy/getById/{id}")]

        public IActionResult GetPolicyById(int id)
        {
            var result = _policyRepository.GetPolicyById(id);

            if (result == null)
            {
                return NotFound($"Policy Not Found of {id}.");
            }
            return Ok(result);
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("/api/v1.0/policy/searches")]

        public IActionResult SearchPolicy([FromQuery] SearchViewModel searchViewModel)
        {
            if (string.IsNullOrWhiteSpace(searchViewModel.PolicyTypeName)
                && !searchViewModel.NumberOfYears.HasValue
                && string.IsNullOrWhiteSpace(searchViewModel.PolicyCompany)
                && !searchViewModel.PolicyId.HasValue
                && string.IsNullOrWhiteSpace(searchViewModel.PolicyName))
            {
                return BadRequest("At least one search criteria is required");
            }

            var result = _policyRepository.SearchPolicy(searchViewModel);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("/api/v1.0/policy/register")]

        public IActionResult PostPolicy(PolicyViewModel policyViewModel)
        {
            if (policyViewModel == null)
            {
                return NotFound("New Customer Not Added.");
            }

            var result = _policyRepository.AddNewPolicy(policyViewModel);

            return Ok(result);
        }
    }
}
