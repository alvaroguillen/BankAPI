
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
        public async Task<IEnumerable<Client>> ObtenerTodo()
        {
            return await _clienteServicio.ObtenerTodo();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> ObtenerPorId(int id)
        {
            var cliente = await _clienteServicio.ObtenerPorId(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Client cliente) 
        {
            var nuevoCliente = await _clienteServicio.Crear(cliente);

            return CreatedAtAction(nameof(ObtenerPorId), new {id = nuevoCliente.Id}, nuevoCliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, Client cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            var actualizarCliente = await _clienteServicio.ObtenerPorId(id);

            if (actualizarCliente is not null)
            {
                await _clienteServicio.Actualizar(id, cliente);
                 return NoContent();
            }else
            {
                return NotFound();
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminarCliente = await _clienteServicio.ObtenerPorId(id);

            if (eliminarCliente is not null)
            {
                await _clienteServicio.Eliminar(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
