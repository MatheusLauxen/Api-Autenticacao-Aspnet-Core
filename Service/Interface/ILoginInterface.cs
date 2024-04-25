using AuthCore.Dto.Login;
using AuthCore.Helpers;
using AuthCore.Models;

namespace AuthCore.Service.Interface
{
    public interface ILoginInterface
    {
        Task<ServiceResponse<SegurancaUsuarioModel>> CreateAccount(CreateAccountDto createAccountDto);
        Task<ServiceResponse<string>> authenticateUser(AuthenticateUserDto authenticateUser);
        string GerarToken(SegurancaUsuarioModel segurancaUsuario);
        Task<ServiceResponse<bool>> RedefinirSenha(RedefinepasswordDto redefinepasswordDto);
    }
}
