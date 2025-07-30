using BankAPI.Data.DTOs;
using BankAPI.Data.BankModels;
using BankAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly LoginServicio loginServicio;

        private IConfiguration config;
        public LoginController(LoginServicio loginServicio, IConfiguration config)
        {
            this.loginServicio = loginServicio;
            this.config = config;
        }

        [HttpPost("autenticacion")]
        public async Task<IActionResult> Login(AdminDto adminDto)
        {
            var admin = await loginServicio.ObtenerAdmin(adminDto);

            if(admin is null)
            {
                //Console.WriteLine("Hash de Admin123: " + BCrypt.Net.BCrypt.HashPassword("Admin123"));

                return BadRequest(new { message = "Credenciales invalidas."});
                //return BadRequest(new { message = BCrypt.Net.BCrypt.HashPassword("Admin123")});
            }

            string jwtToken = GenerarToken(admin);

            return Ok(new {token = jwtToken});
        }

        private string GenerarToken(Administrator admin)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, admin.Name),
                new Claim(ClaimTypes.Email, admin.Email),
                new Claim("AdminType", admin.AdminType)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }
    }
}
