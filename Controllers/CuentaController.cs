using BankAPI.Data.BankModels;
using BankAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CuentaController:ControllerBase
    {
        private readonly CuentaServicio cuentaServicio;
        private readonly TipoCuentaServicio tipoCuentaServicio;
        private readonly ClienteServicio clienteServicio;

        public CuentaController(CuentaServicio cuentaServicio, TipoCuentaServicio tipoCuentaServicio, ClienteServicio clienteServicio)
        {
            this.cuentaServicio = cuentaServicio;
            this.tipoCuentaServicio = tipoCuentaServicio;
            this.clienteServicio = clienteServicio;
        }

        [HttpGet]
        public async Task<IEnumerable<Account>> Obtener()
        {
            return await cuentaServicio.ObtenerTodo();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> ObtenerPorId(int id)
        {
            var cuenta = await cuentaServicio.ObtenerPorId(id);

            if (cuenta == null)
            {
                return AccountNotFound(id);
            }

            return Ok(cuenta);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Account cuenta)
        {
            string ResultadoValidacion = await ValidacionCuenta(cuenta);
            if (!ResultadoValidacion.Equals("valido"))
            {
                return BadRequest(new { message = ResultadoValidacion });
            }
            var nuevaCuenta = await cuentaServicio.Crear(cuenta);

            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevaCuenta.Id }, nuevaCuenta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, Account cuenta)
        {
            if (id != cuenta.Id)
            {
                return BadRequest(new { message = $"El ID({id}) de la URL no coincide con el ID({cuenta.Id}) del cuerpo de la solicitud." });
            }

            var actualizarCuenta = await cuentaServicio.ObtenerPorId(id);

            if (actualizarCuenta is not null)
            {
                string ResultadoValidacion = await ValidacionCuenta(cuenta);

                if (!ResultadoValidacion.Equals("valido"))
                {
                    return BadRequest(new { message = ResultadoValidacion });
                }

                await cuentaServicio.Actualizar(id, cuenta);
                return NoContent();
            }
            else
            {
                return AccountNotFound(id);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminarCuenta = await cuentaServicio.ObtenerPorId(id);

            if (eliminarCuenta is not null)
            {
                await cuentaServicio.Eliminar(id);
                return Ok();
            }
            else
            {
                return AccountNotFound(id);
            }
        }

        public NotFoundObjectResult AccountNotFound(int id)
        {
            return NotFound(new { message = $"La cuenta con ID = {id} no existe." });
        }

        public async Task<string> ValidacionCuenta(Account cuenta)
        {
            string resultado = "valido";
            var tipoCuenta = await tipoCuentaServicio.ObtenerPorId(cuenta.AccountType);
            
            if (tipoCuenta is null)
            {
                resultado = $"El tipo de cuenta {cuenta.AccountType} no existe.";
            }

            var clienteID = cuenta.ClientId.GetValueOrDefault();

            var cliente = clienteServicio.ObtenerPorId(clienteID);

            if (cliente is null)
            {
                resultado = $"El cliente con ID({clienteID} no existe.)";
            }

            return resultado;
        }
    }
}
