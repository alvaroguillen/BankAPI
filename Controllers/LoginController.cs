using BankAPI.Data.DTOs;
using BankAPI.Data.BankModels;
using BankAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly LoginServicio loginServicio;
        public LoginController(LoginServicio loginServicio)
        {
            this.loginServicio = loginServicio;
        }

        [HttpPost("autenticacion")]
        public async Task<IActionResult> Login(AdminDto adminDto)
        {
            var admin = await loginServicio.ObtenerAdmin(adminDto);

            if(admin is null)
            {
                return BadRequest(new { message = "Credenciales invalidas."});
            }

            return Ok(new {token = "algun valor"});
        }
    }
}
