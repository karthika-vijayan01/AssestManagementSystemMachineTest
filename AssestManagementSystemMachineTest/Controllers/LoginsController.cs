using AssestManagementSystemMachineTest.Models;
using AssestManagementSystemMachineTest.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AssestManagementSystemMachineTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILoginRepository _loginRepository;

        public LoginsController(IConfiguration config, ILoginRepository loginRepository)
        {
            _config = config;
            _loginRepository = loginRepository;
        }

        [HttpGet("{userName}/{passWord}")]
        [AllowAnonymous] // Allow unauthenticated access
        public async Task<IActionResult> LoginCredential(string userName, string passWord)
        {
            IActionResult response = Unauthorized(); // Default response is 401 Unauthorized

            // Validate user credentials
            LoginUser validUser = await _loginRepository.ValidateUsers(userName, passWord);

            if (validUser != null)
            {
                // Generate JWT Token
                string tokenString = GenerateJWTToken(validUser);

                response = Ok(new
                {
                    Uname = validUser.UserName,
                    UserPass = validUser.UserPass,
                    token = tokenString
                });
            }

            return response;
        }

        private string GenerateJWTToken(LoginUser validUser)
        {
            // Secret key for signing the token
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"]));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            // Create the token
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: null, // Add claims if needed
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials
            );

            // Write and return the token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


}





















