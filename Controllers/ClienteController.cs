using BankAPI.Data;
using BankAPI.Data.BankModels;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("controller")]
    public class ClienteController : ControllerBase
    {
        private readonly BancoContext _bancoContext;
        public ClienteController(BancoContext bancoContexto)
        {
            _bancoContext = bancoContexto;
        }
        [HttpGet]
        public IEnumerable<Cliente> ObtenerTodo() 
        { 
            return _bancoContext.Clientes.ToList();
        }
    }
}
