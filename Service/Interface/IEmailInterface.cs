namespace AuthCore.Service.Interface
{
    public interface IEmailInterface
    {
        bool Enviar(string email, string assunto, string mensagem);
    }
}
