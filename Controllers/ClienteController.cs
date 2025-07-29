
using BankAPI.Data.BankModels;
using BankAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteServicio _clienteServicio;
        public ClienteController(ClienteServicio servicio)
        {
            _clienteServicio = servicio;
        }

        [HttpGet("obtenerTodo")]
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
                return ClientNotFound(id);
            }

            return Ok(cliente);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> Crear(Client cliente) 
        {
            var nuevoCliente = await _clienteServicio.Crear(cliente);

            return CreatedAtAction(nameof(ObtenerPorId), new {id = nuevoCliente.Id}, nuevoCliente);
        }

        [HttpPut("editar/{id}")]
        public async Task<IActionResult> Actualizar(int id, Client cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest(new {message = $"El ID({id}) de la URL no coincide con el ID({cliente.Id}) del cuerpo de la solicitud."});
            }

            var actualizarCliente = await _clienteServicio.ObtenerPorId(id);

            if (actualizarCliente is not null)
            {
                await _clienteServicio.Actualizar(id, cliente);
                 return NoContent();
            }else
            {
                return ClientNotFound(id);
            }

        }

        [HttpDelete("eliminar/{id}")]
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
                return ClientNotFound(id);
            }
        }

        public NotFoundObjectResult ClientNotFound(int id)
        {
            return NotFound(new { message = $"El cliente con ID = {id} no existe."});
        }
    }
}
