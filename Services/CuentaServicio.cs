using BankAPI.Data;
using BankAPI.Data.BankModels;
using BankAPI.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services
{
    public class CuentaServicio
    {
        private readonly BancoDbContext _bancoDbContext;

        public CuentaServicio(BancoDbContext bancoDbContext)
        {
            _bancoDbContext = bancoDbContext;
        }

        public async Task<IEnumerable<Account>> ObtenerTodo()
        {
            return await _bancoDbContext.Accounts.ToListAsync();
        }

        public async Task<Account?> ObtenerPorId(int id)
        {
            return await _bancoDbContext.Accounts.FindAsync(id);
        }

        public async Task<Account> Crear(CuentaDtoIn nuevaCuentaDTO)
        {
            var nuevaCuenta = new Account();

            nuevaCuenta.AccountType = nuevaCuentaDTO.AccountType;
            nuevaCuenta.ClientId = nuevaCuentaDTO.ClientId;
            nuevaCuenta.Balance = nuevaCuentaDTO.Balance;

            _bancoDbContext.Accounts.Add(nuevaCuenta);
            await _bancoDbContext.SaveChangesAsync();

            return nuevaCuenta;
        }

        public async Task Actualizar(int id, CuentaDtoIn cuentaDTO)
        {
            var ExisteCuenta = await ObtenerPorId(id);

            if (ExisteCuenta is not null)
            {
                ExisteCuenta.AccountType = cuentaDTO.AccountType;
                ExisteCuenta.ClientId = cuentaDTO.ClientId;
                ExisteCuenta.Balance= cuentaDTO.Balance;

                await _bancoDbContext.SaveChangesAsync();
            }
        }

        public async Task Eliminar(int id)
        {
            var eliminarCuenta= await ObtenerPorId(id);

            if (eliminarCuenta is not null)
            {
                _bancoDbContext.Accounts.Remove(eliminarCuenta);
                await _bancoDbContext.SaveChangesAsync();

            }
        }
    }
}
