using AuthCore.Helpers;
using System.ComponentModel.DataAnnotations;

namespace AuthCore.Dto.Login
{
    public class RedefinepasswordDto
    {
        [Required(ErrorMessage = "O campo cpf é obrigatório.")]
        public string cpf { get; set; }

        [Required(ErrorMessage = "O campo email é obrigatório.")]
        public string email { get; set; }

    }
}
