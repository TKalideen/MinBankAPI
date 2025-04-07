using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MinBankAPI.Interfaces;
using MinBankAPI.Models;

namespace MinBankAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class LoginAPIController : ControllerBase
    {
        private readonly IAPIAuthRepository _aPIAuthRepository;

        public LoginAPIController(IAPIAuthRepository apiAuth)
        {
            _aPIAuthRepository = apiAuth;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserforAPI model)
        {
            var token = _aPIAuthRepository.Login(model.Username, model.Password);
            if (token == null) return Unauthorized("Invalid credentials");

            return Ok(new { Token = token });
        }
    }
}
