using BankAPI.Data;
using BankAPI.Data.BankModels;

namespace BankAPI.Services
{
    public class TipoCuentaServicio
    {
        private readonly BancoDbContext _bancoDbContext;

        public TipoCuentaServicio(BancoDbContext bancoDbContext)
        {
            _bancoDbContext = bancoDbContext;
        }

        public async Task<AccountType?> ObtenerPorId(int id)
        {
            return await _bancoDbContext.AccountTypes.FindAsync(id);
        }
    }
}
