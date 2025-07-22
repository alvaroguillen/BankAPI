using BankAPI.Data;
using BankAPI.Data.BankModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services;
public class ClienteServicio
{
    private readonly BancoDbContext _bancoDbContext;

    public ClienteServicio(BancoDbContext bancoDbContext)
    {
        _bancoDbContext = bancoDbContext; 
    }

    public async Task<IEnumerable<Client>> ObtenerTodo()
    {
        return await _bancoDbContext.Clients.ToListAsync();
    }

    public async Task<Client?> ObtenerPorId(int id)
    {
        return await _bancoDbContext.Clients.FindAsync(id);
    }

    public async Task<Client> Crear(Client nuevoCliente)
    {
        _bancoDbContext.Clients.Add(nuevoCliente);
        await _bancoDbContext.SaveChangesAsync();

        return nuevoCliente;
    }

    public async Task Actualizar(int id, Client cliente)
    {
        var ExisteCliente = await ObtenerPorId(id);

        if (ExisteCliente is not null)
        {
            ExisteCliente.Name = cliente.Name;
            ExisteCliente.PhoneNumber = cliente.PhoneNumber;
            ExisteCliente.Email = cliente.Email;

            await _bancoDbContext.SaveChangesAsync();
        }
    }

    public async Task Eliminar(int id)
    {
        var eliminarCliente = await ObtenerPorId(id);

        if (eliminarCliente is not null)
        {
            _bancoDbContext.Clients.Remove(eliminarCliente);
            await _bancoDbContext.SaveChangesAsync();

        }
    }
}
