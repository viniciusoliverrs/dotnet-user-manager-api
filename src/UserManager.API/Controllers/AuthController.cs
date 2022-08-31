using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserManager.API.Token;
using UserManager.API.ViewModels;

namespace UserManager.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthController(IConfiguration configuration, ITokenGenerator tokenGenerator)
        {
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginViewModel loginViewModel)
        {
            if (loginViewModel.Email == _configuration["Jwt:Email"] && loginViewModel.Password == _configuration["Jwt:Password"])
            {
                return Ok(new
                {
                    token = _tokenGenerator.GenerateToken(),
                    TokenExpiration = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:ExpireHours"]))
                });
            }
            return Unauthorized();
        }
    }
}