using AuthCore.Data;
using AuthCore.Dto.Login;
using AuthCore.Helpers;
using AuthCore.Models;
using AuthCore.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthCore.Service
{
    public class LoginService : ILoginInterface
    {
        private readonly BancoContext _bancoContext;
        private IConfiguration _config;
        private readonly IEmailInterface _emailInterface;
        public LoginService(BancoContext bancoContext, IConfiguration config, IEmailInterface emailInterface)
        {
            _bancoContext = bancoContext;
            _config = config;
            _emailInterface = emailInterface;
        }

        public async Task<ServiceResponse<SegurancaUsuarioModel>> CreateAccount(CreateAccountDto createAccountDto)
        {
            ServiceResponse<SegurancaUsuarioModel> serviceResponse = new ServiceResponse<SegurancaUsuarioModel>();

            try
            {
                var usuarioExistente = await _bancoContext.SegurancaUsuario.FirstOrDefaultAsync(u => u.cpf == createAccountDto.cpf);

                if (usuarioExistente != null)
                {
                    serviceResponse.mensagem = "Já existe um usuário com esse mesmo cpf registrado na base de dados!";
                    serviceResponse.sucesso = false;

                    return serviceResponse;
                }

                string senhaCriptografada = EncryptPassword.Encode(createAccountDto.senha);

                var usuario = new SegurancaUsuarioModel()
                {
                    nome = createAccountDto.nome,
                    cpf = createAccountDto.cpf,
                    email = createAccountDto.email,
                    senha = senhaCriptografada,
                    dtaCriacao = DateTime.Now
                };

                _bancoContext.Add(usuario);
                await _bancoContext.SaveChangesAsync();

                serviceResponse.dados = usuario;
                serviceResponse.mensagem = "Usuário cadastrado com sucesso!";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<string>> authenticateUser(AuthenticateUserDto authenticateUser)
        {
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();

            try
            {
                var usuario = await _bancoContext.SegurancaUsuario.FirstOrDefaultAsync(x => x.email == authenticateUser.email);

                if (usuario == null)
                {
                    serviceResponse.mensagem = "Usuário não cadastrado na base de dados!";
                    serviceResponse.sucesso = false;

                    return serviceResponse;
                }

                string senhaCriptografada = EncryptPassword.Encode(authenticateUser.senha);

                if (senhaCriptografada != null && usuario.senha == senhaCriptografada)
                {
                    var token = GerarToken(usuario);

                    serviceResponse.dados = token;
                    serviceResponse.mensagem = $"Usuário logado com sucesso!";
                }
                else
                {
                    serviceResponse.mensagem = "Senha incorreta!";
                    serviceResponse.sucesso = false;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.mensagem = ex.Message;
                serviceResponse.sucesso = false;
            }

            return serviceResponse;
        }

        public string GerarToken(SegurancaUsuarioModel segurancaUsuario)
        {
            var tokenKey = _config["AppSettings:Token"];

            var claims = new List<Claim>
            {
                new Claim("nome", segurancaUsuario.nome),
                new Claim("cpf", segurancaUsuario.cpf),
                new Claim("email", segurancaUsuario.email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenKey));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<ServiceResponse<bool>> RedefinirSenha(RedefinepasswordDto redefinepasswordDto)
        {
            ServiceResponse<bool> serviceResponse = new ServiceResponse<bool>();

            try
            {
                var usuario = await _bancoContext.SegurancaUsuario.FirstOrDefaultAsync(u => u.cpf == redefinepasswordDto.cpf && u.email == redefinepasswordDto.email);

                if (usuario != null)
                {
                    string novaSenha = usuario.GerarNovaSenha();

                    string mensagem = $@"
                    <h2><strong>Alteração de Senha</strong></h2>
                    <br>
                    Você solicitou a alteração de senha em nosso sistema:
                    <br><br>
                    Nome completo: {usuario.nome}
                    <br>
                    E-mail: {usuario.email}
                    <br><br>
                    Sua nova senha é: {novaSenha}";

                    bool emailEnviado = _emailInterface.Enviar(usuario.email, "Alteração de Senha - API", mensagem);

                    if (emailEnviado)
                    {
                        usuario.senha = EncryptPassword.Encode(novaSenha);
                        _bancoContext.SaveChanges();

                        serviceResponse.dados = true;
                        serviceResponse.mensagem = "Senha redefinida com sucesso!";
                    }
                    else
                    {
                        serviceResponse.dados = false;
                        serviceResponse.mensagem = "Não foi possível enviar o e-mail de redefinição de senha. Por favor, tente novamente!";
                    }
                }
                else
                {
                    serviceResponse.dados = false;
                    serviceResponse.mensagem = "Usuário não encontrado ou email incorreto!";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.dados = false;
                serviceResponse.mensagem = $"Ocorreu um erro ao tentar redefinir a senha: {ex.Message}";
            }

            return serviceResponse;
        }

    }
}
