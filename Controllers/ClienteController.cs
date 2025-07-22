
using BankAPI.Services;
using BankAPI.Data.BankModels;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteServicio _clienteServicio;
        public ClienteController(ClienteServicio servicio)
        {
            _clienteServicio = servicio;
        }
        [HttpGet]
        public IEnumerable<Client> ObtenerTodo()
        {
            return _clienteServicio.ObtenerTodo();
        }

        [HttpGet("{id}")]
        public ActionResult<Client> ObtenerPorId(int id)
        {
            var cliente = _clienteServicio.ObtenerPorId(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult Crear(Client cliente) 
        {
            var nuevoCliente = _clienteServicio.Crear(cliente);

            return CreatedAtAction(nameof(ObtenerPorId), new {id = nuevoCliente.Id}, nuevoCliente);
        }

        [HttpPut("{id}")]
        public IActionResult Actualizar(int id, Client cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            var actualizarCliente = _clienteServicio.ObtenerPorId(id);

            if (actualizarCliente is not null)
            {
                _clienteServicio.Actualizar(id, cliente);
                 return NoContent();
            }else
            {
                return NotFound();
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            var eliminarCliente = _clienteServicio.ObtenerPorId(id);

            if (eliminarCliente is not null)
            {
                _clienteServicio.Eliminar(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
