
using BankAPI.Data;
using BankAPI.Data.BankModels;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly BancoDbContext _bancoDbContext;
        public ClienteController(BancoDbContext bancoDbContext)
        {
            _bancoDbContext = bancoDbContext;
        }
        [HttpGet]
        public IEnumerable<Client> ObtenerTodo()
        {
            return _bancoDbContext.Clients.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Client> ObtenerPorId(int id)
        {
            var cliente = _bancoDbContext.Clients.Find(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult Crear(Client cliente) 
        { 
            _bancoDbContext.Clients.Add(cliente);
            _bancoDbContext.SaveChanges();

            return CreatedAtAction(nameof(ObtenerPorId), new {id = cliente.Id}, cliente);
        }

        [HttpPut("{id}")]
        public IActionResult Actualizar(int id, Client cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            var ExisteCliente = _bancoDbContext.Clients.Find(id);

            if (ExisteCliente == null)
            {
                return NotFound();
            }

            ExisteCliente.Name = cliente.Name;
            ExisteCliente.PhoneNumber = cliente.PhoneNumber;
            ExisteCliente.Email = cliente.Email;

            _bancoDbContext.SaveChanges();

            return NoContent();
        }
    }
}
