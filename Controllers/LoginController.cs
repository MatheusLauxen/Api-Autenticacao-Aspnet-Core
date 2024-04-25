using AuthCore.Dto.Login;
using AuthCore.Helpers;
using AuthCore.Models;
using AuthCore.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginInterface _loginInterface;
        public LoginController(ILoginInterface login)
        {
            _loginInterface = login;
        }

        [HttpPost("CreateAccount")]
        public async Task<ActionResult<ServiceResponse<SegurancaUsuarioModel>>> CreateUsuario(CreateAccountDto createAccountDto)
        {
            var usuario = await _loginInterface.CreateAccount(createAccountDto);
            return Ok(usuario);
        }

        [HttpPost("AuthenticateUser")]
        public async Task<ActionResult> AuthenticateUser(AuthenticateUserDto authenticateUser)
        {
            var resposta = await _loginInterface.authenticateUser(authenticateUser);
            return Ok(resposta);
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult<ServiceResponse<bool>>> RedefinirSenha(RedefinepasswordDto redefinepasswordDto)
        {
            var resposta = await _loginInterface.RedefinirSenha(redefinepasswordDto);
            return Ok(resposta);
        }

    }
}
