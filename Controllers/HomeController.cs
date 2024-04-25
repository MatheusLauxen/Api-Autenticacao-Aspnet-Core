using AuthCore.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthCore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("Index")]
        public ActionResult<ServiceResponse<string>> Index()
        {
            var nomeClaim = User.Claims.FirstOrDefault(c => c.Type == "nome");

            if (nomeClaim != null)
            {
                return Ok(new ServiceResponse<string> { mensagem = $"Olá, {nomeClaim.Value}! Você acessou o sistema com sucesso." });
            }
            else
            {
                return BadRequest(new ServiceResponse<string> { mensagem = "Erro ao recuperar o nome do usuário." });
            }
        }
    }
}
