using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POlidvyAPI.Repository;
using POlidvyAPI.ViewModels;

namespace POlidvyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenHandler _tokenHandler;
        private readonly ILoginRepository _loginRepository;

        public AuthController(ITokenHandler tokenHandler, ILoginRepository loginRepository)
        {
            _tokenHandler = tokenHandler;
            _loginRepository = loginRepository;
        }

        [HttpPost("/api/v1.0/Login")]

        public IActionResult Login(LoginViewModel loginViewModel)
        {
            var result = _loginRepository.Authenticate(loginViewModel);

            if(result == null)
            {
                return BadRequest("Username or Password is incorrect.");
            }

            return Ok(_tokenHandler.CreateToken(result));
        }
    }
}
