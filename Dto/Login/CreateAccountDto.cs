using System.ComponentModel.DataAnnotations;

namespace AuthCore.Dto.Login
{
    public class CreateAccountDto
    {
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [StringLength(150, ErrorMessage = "O campo nome permite até 150 caracteres.")]
        public string nome { get; set; }

        [Required(ErrorMessage = "O campo cpf é obrigatório.")]
        [StringLength(150, ErrorMessage = "O campo cpf permite até 11 caracteres.")]
        public string cpf { get; set; }

        [Required(ErrorMessage = "O campo email é obrigatório.")]
        [StringLength(150, ErrorMessage = "O campo email permite até 150 caracteres.")]
        public string email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        [StringLength(150, ErrorMessage = "O campo senha permite até 250 caracteres.")]
        public string senha { get; set; }
    }
}
