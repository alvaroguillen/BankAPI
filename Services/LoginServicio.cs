using BankAPI.Data;
using BankAPI.Data.BankModels;
using BankAPI.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services
{
    public class LoginServicio
    {
        private readonly BancoDbContext _bancoDbContext;
        public LoginServicio(BancoDbContext bancoDbContext)
        {
            _bancoDbContext = bancoDbContext;
        }

        public async Task<Administrator?> ObtenerAdmin(AdminDto admin)
        {

            var adminEncontrado = await _bancoDbContext.Administrators.SingleOrDefaultAsync(a => a.Email  == admin.Email);

            if(adminEncontrado == null)
            {
                return null;
            }

            bool esValido = BCrypt.Net.BCrypt.Verify(admin.Password, adminEncontrado.PwdHash);

            if(esValido == false)
            {
                return null;
            }

            return adminEncontrado;

        }
    }
}
