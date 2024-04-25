using System.ComponentModel.DataAnnotations;

namespace AuthCore.Dto.Login
{
    public class AuthenticateUserDto
    {
        [Required(ErrorMessage = "O campo email é obrigatório.")]
        public string email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        public string senha { get; set; }
    }
}
