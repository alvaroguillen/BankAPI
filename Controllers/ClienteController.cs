
using BankAPI.Data;
using BankAPI.Data.BankModels;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("controller")]
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
    }
}
