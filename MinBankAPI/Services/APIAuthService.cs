using Microsoft.IdentityModel.Tokens;
using MinBankAPI.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinBankAPI.Services
{
    public class APIAuthService : IAPIAuthRepository
    {
        private readonly IConfiguration _configuration;

        public APIAuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Login(string username, string password)
        {
            var predefinedUsername = _configuration["LoginForAPI:Username"];
            var predefinedPassword = _configuration["LoginForAPI:Password"];

            if (username != predefinedUsername || password != predefinedPassword)
            {
                return null;  // Invalid credentials
            }

            return GenerateJwtToken(username);  // Generate JWT token on successful login
        }

        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, username)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
