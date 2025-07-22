using BankAPI.Data;
using BankAPI.Data.BankModels;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Services;
public class ClienteServicio
{
    private readonly BancoDbContext _bancoDbContext;

    public ClienteServicio(BancoDbContext bancoDbContext)
    {
        _bancoDbContext = bancoDbContext; 
    }

    public IEnumerable<Client> ObtenerTodo()
    {
        return _bancoDbContext.Clients.ToList();
    }

    public Client? ObtenerPorId(int id)
    {
        return _bancoDbContext.Clients.Find(id);

    }

    public Client Crear(Client nuevoCliente)
    {
        _bancoDbContext.Clients.Add(nuevoCliente);
        _bancoDbContext.SaveChanges();

        return  nuevoCliente;
    }

    public void Actualizar(int id, Client cliente)
    {
        var ExisteCliente = ObtenerPorId(id);

        if (ExisteCliente is not null)
        {
            ExisteCliente.Name = cliente.Name;
            ExisteCliente.PhoneNumber = cliente.PhoneNumber;
            ExisteCliente.Email = cliente.Email;

            _bancoDbContext.SaveChanges();
        }
    }

    public void Eliminar(int id)
    {
        var eliminarCliente = _bancoDbContext.Clients.Find(id);

        if (eliminarCliente is not null)
        {
            _bancoDbContext.Clients.Remove(eliminarCliente);
            _bancoDbContext.SaveChanges();

        }
    }
}
